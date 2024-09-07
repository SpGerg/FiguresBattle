namespace Server.Controllers.Databases.Interfaces
{
    using Server.Controllers.Authentications.Datas;

    public interface IDatabase
    {
        bool IsCanLogin(string username, string password);

        bool IsCanLogin(string jwt);

        void Register(string username, string password);

        User GetUser(string username);

        void Delete(string username, string password);
    }
}
