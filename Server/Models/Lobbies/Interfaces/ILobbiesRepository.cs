namespace Server.Models.Lobbies.Interfaces
{
    using Server.Models.Interfaces;
    using Server.Models.Lobbies.Datas;

    public interface ILobbiesRepository : IRepository<Lobby>
    {
        Lobby? GetById(int id);

        Lobby? GetLobbyWithAccount(string username);

        int GetUniqueId();
    }
}
