using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Services.Accounts
{
    using Server.Controllers.Databases.Interfaces;
    using Server.Services.Accounts.Datas;

    public class AccountsService(ILogger<AccountsService> logger, IAsyncDatabase database)
    {
        public static SymmetricSecurityKey SymmetricSecurityKey => new(SecretKeyInBytes);

        public static byte[] SecretKeyInBytes => Encoding.UTF8.GetBytes(SecretKey);

        public static string SecretKey => Environment.GetEnvironmentVariable("FIGURES_BATTLE_SECRET_KEY");

        public const string Issuer = "Figures Battle Server";
        public const string Audience = "Figures Battle Client";

        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        private readonly int _jwtTokenDurationInDays = 14;

        public ILogger<AccountsService> Logger => logger;

        public async Task<string> Login(string username, string password)
        {
            await database.Login(username, password);

            var claims = new List<Claim>()
            {
                new("username", username),
                new("password", password)
            };

            var duration = DateTime.UtcNow.AddDays(_jwtTokenDurationInDays);
            var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.Sha256);

            var jwt = new JwtSecurityToken(Issuer, Audience, claims, null, duration, signingCredentials);

            var serialized = _tokenHandler.WriteToken(jwt);

            return serialized;
        }

        public async Task<Account> GetUser(string username)
        {
            return await database.GetAccount(username);
        }
    }
}
