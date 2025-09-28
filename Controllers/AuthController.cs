using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Tibaks_Backend.Auth;
using Tibaks_Backend.Auth.Services;
using Tibaks_Backend.Data;
using Tibaks_Backend.Models;
using Tibaks_Backend.Models.Auth;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;

namespace Tibaks_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        
        public class TokenResponse
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
            public string TokenType { get; set; } = "Bearer";
            public int ExpiresIn { get; set; }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine($"Login attempt for email: {request.Email}");
            
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return Unauthorized();
            }
            
            Console.WriteLine($"User found: {user.Email}");

            
            bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            Console.WriteLine($"Password verification result: {passwordValid}");
            
            if (string.IsNullOrWhiteSpace(user.PasswordHash) || !passwordValid)
            {
                Console.WriteLine("Invalid password");
                return Unauthorized();
            }
            
            Console.WriteLine("Login successful, generating tokens...");

            var appUser = new ApplicationUser { Id = user.Id.ToString(), Email = user.Email };
            var accessToken = _jwtService.GenerateAccessToken(appUser);
            var refreshToken = _jwtService.GenerateRefreshToken(HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");

            var refresh = new RefreshToken
            {
                Token = refreshToken.Token,
                UserId = appUser.Id,
                Expires = refreshToken.Expires,
                Created = refreshToken.Created,
                CreatedByIp = refreshToken.CreatedByIp,
                IsRevoked = false
            };
            _db.RefreshTokens.Add(refresh);
            await _db.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refresh.Token,
                ExpiresIn = (int)TimeSpan.FromMinutes(double.Parse(HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:AccessTokenExpirationMinutes"]!)).TotalSeconds
            };
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> SignUp([FromBody] SignupRequest request)
        {
            Console.WriteLine($"Received: {request.Email}, {request.Email}");
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Email and password are required." );

            }
            if (request.Password.Length < 6)
                return BadRequest("Password must be at least 6 characters long.");

            if (!IsValidEmail(request.Email))
                return BadRequest("Invalid email format.");

            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return Conflict("Email is already registered.");

            try
            {
                string passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var newUser = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordhash
                };
                _db.Users.Add(newUser);
                await _db.SaveChangesAsync();
                return Ok(new { message = "User registered successfully", userId = newUser.Id });
            }
            catch (Exception)
            {
                
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public class RefreshRequest
        {
            public string RefreshToken { get; set; } = string.Empty;
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponse>> Refresh([FromBody] RefreshRequest request)
        {
            var existing = await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken);
            if (existing == null || existing.IsRevoked || existing.Expires < DateTime.UtcNow)
                return Unauthorized();

            var user = await _db.Users.FindAsync(int.Parse(existing.UserId));
            if (user == null)
                return Unauthorized();

            var appUser = new ApplicationUser { Id = existing.UserId, Email = user.Email };
            var newAccess = _jwtService.GenerateAccessToken(appUser);
            var newRefresh = _jwtService.GenerateRefreshToken(HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");

            existing.IsRevoked = true;
            _db.RefreshTokens.Update(existing);
            _db.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefresh.Token,
                UserId = appUser.Id,
                Expires = newRefresh.Expires,
                Created = newRefresh.Created,
                CreatedByIp = newRefresh.CreatedByIp,
                IsRevoked = false
            });
            await _db.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = newAccess,
                RefreshToken = newRefresh.Token,
                ExpiresIn = (int)TimeSpan.FromMinutes(double.Parse(HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:AccessTokenExpirationMinutes"]!)).TotalSeconds
            };
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RefreshRequest request)
        {
            var token = await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken);
            if (token == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (token.UserId != userId)
                return Forbid();

            token.IsRevoked = true;
            _db.RefreshTokens.Update(token);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}


