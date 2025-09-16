Authentication (JWT) Setup
\nThis project uses JSON Web Tokens (JWT) for stateless authentication and refresh tokens stored in the database.
\nWhat was added
- JWT configuration in `appsettings.json` under `Jwt`.
- JWT bearer authentication wired in `Program.cs` (`AddAuthentication().AddJwtBearer(...)`).
- `Auth/Services/JwtService.cs` completed to create access and refresh tokens and parse expired tokens.
- `Controllers/AuthController.cs` with endpoints: `POST /api/auth/login`, `POST /api/auth/refresh`, `POST /api/auth/revoke`.
- `[Authorize]` added to `PatientsController` so it requires a valid access token.
\nConfiguration
Update these values in `appsettings.json`:
```json
"Jwt": {
  "Key": "CHANGE_ME_dev_secret_key_min_32_chars_long_123456",
  "Issuer": "Tibaks",
  "Audience": "TibaksClient",
  "AccessTokenExpirationMinutes": 60,
  "RefreshTokenExpirationDays": 7
}
```
- Key must be at least 32 chars for HS256. Use user-secrets or env vars in production.
\nEndpoints
- POST `/api/auth/login`
  - body: `{ "email": "user@example.com", "password": "<password>" }`
  - returns: `{ accessToken, refreshToken, tokenType: "Bearer", expiresIn }`
- POST `/api/auth/refresh`
  - body: `{ "refreshToken": "..." }`
  - returns new `{ accessToken, refreshToken, ... }` and revokes the old refresh token
- POST `/api/auth/revoke`
  - body: `{ "refreshToken": "..." }`
  - requires Authorization header; revokes the refresh token for the current user
\nUsing the access token
Send the header:
```
Authorization: Bearer <accessToken>
```
`PatientsController` now requires this header for all actions.
\nUsers and passwords
- This project has a simple `Users` table (`Models/User.cs`). The current login implementation compares `PasswordHash` directly with the provided password for simplicity. Replace with a secure hash (e.g., BCrypt) in production.
\nImplementation notes
- Auth is configured in `Program.cs`, and the service `JwtService` reads values from configuration.
- Refresh tokens are persisted in `ApplicationDbContext` via the `RefreshTokens` table.
\nQuick test (via Tibaks Backend.http)
Add requests like:
```
POST https://localhost:5001/api/auth/login
Content-Type: application/json

{ "email": "test@example.com", "password": "password" }
```
Then use the returned access token to call any `api/Patients` endpoints with the Authorization header.


