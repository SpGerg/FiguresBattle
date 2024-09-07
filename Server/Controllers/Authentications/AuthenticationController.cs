using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Authentication
{
    using Microsoft.IdentityModel.Tokens;
    using Server.Controllers.Databases.Interfaces;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IDatabase database) : ControllerBase
    {
        public static SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(SecretKeyInBytes);

        public static byte[] SecretKeyInBytes => Encoding.UTF8.GetBytes(SecretKey);

        public static string SecretKey => Environment.GetEnvironmentVariable("FIGURES_BATTLE_SECRET_KEY");

        public const string Issuer = "Figures Battle Server";
        public const string Audience = "Figures Battle Client";

        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        private readonly IDatabase _database = database;

        private readonly int _jwtTokenTimeInDays = 14;

        [HttpPost("login")]
        public Task<ActionResult<string>> PostLogin(string username, string password)
        {
            var claims = new List<Claim>()
            {
                new("username", username),
                new("password", password)
            };

            var duration = DateTime.UtcNow.AddDays(_jwtTokenTimeInDays);
            var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.Sha256);

            var jwt = new JwtSecurityToken(Issuer, Audience, claims, null, duration, signingCredentials);

            var serializaed = _tokenHandler.WriteToken(jwt);

            return Task.FromResult<ActionResult<string>>(Ok(serializaed));
        }
    }
} 
