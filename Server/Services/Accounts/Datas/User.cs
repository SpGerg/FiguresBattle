namespace Server.Services.Accounts.Datas
{
    public class User
    {
        public required string Username { get; init; }

        public required string EncryptedPassword { get; init; }

        public required string Country { get; init; }
    }
}
