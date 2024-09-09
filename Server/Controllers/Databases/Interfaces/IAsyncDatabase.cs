namespace Server.Controllers.Databases.Interfaces
{
    using Server.Services.Accounts.Datas;

    public interface IAsyncDatabase
    {
        Task Login(string username, string password);

        Task Register(string username, string password, string country);

        Task<Account> GetAccount(string username);

        Task DeleteAccount(string username, string password);
    }
}
