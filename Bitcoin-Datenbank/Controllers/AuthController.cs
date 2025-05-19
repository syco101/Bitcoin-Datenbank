using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AksicDavid_LB_M295.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private const string SECRET_KEY = "this_is_a_very_secure_256_bit_key!"; // Gleicher Schlüssel wie in Program.cs
        private static Dictionary<string, string> refreshTokens = new Dictionary<string, string>();

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "password")
            {
                var accessToken = GenerateToken(request.Username, TimeSpan.FromMinutes(30));
                var refreshToken = GenerateToken(request.Username, TimeSpan.FromDays(7));

                refreshTokens[refreshToken] = request.Username;

                return Ok(new { accessToken, refreshToken });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshRequest request)
        {
            if (refreshTokens.TryGetValue(request.RefreshToken, out string username))
            {
                var newAccessToken = GenerateToken(username, TimeSpan.FromMinutes(30));
                return Ok(new { accessToken = newAccessToken });
            }

            return Unauthorized("Invalid refresh token");
        }

        private string GenerateToken(string username, TimeSpan expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SECRET_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.Add(expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RefreshRequest
    {
        public string RefreshToken { get; set; }
    }
}
