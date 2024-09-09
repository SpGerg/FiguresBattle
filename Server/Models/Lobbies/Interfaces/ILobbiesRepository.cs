namespace Server.Models.Lobbies.Interfaces
{
    using Server.Models.Interfaces;
    using Server.Models.Lobbies.Datas;

    public interface ILobbiesRepository : IRepository<Lobby>
    {
        Lobby? Get(int id);

        int GetUniqueId();
    }
}
