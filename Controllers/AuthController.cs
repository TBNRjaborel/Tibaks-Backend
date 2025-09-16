using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Tibaks_Backend.Auth;
using Tibaks_Backend.Auth.Services;
using Tibaks_Backend.Data;
using Tibaks_Backend.Models;

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

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class TokenResponse
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public string TokenType { get; set; } = "Bearer";
            public int ExpiresIn { get; set; }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized();

            // For demo: PasswordHash assumed to be a plain hash; replace with BCrypt verification if used
            // Here we simply compare for now
            if (string.IsNullOrWhiteSpace(user.PasswordHash) || user.PasswordHash != request.Password)
                return Unauthorized();

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


