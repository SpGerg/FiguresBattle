namespace Server.Models.Lobbies.Interfaces
{
    using Server.Models.Interfaces;
    using Server.Models.Lobbies.Datas;

    public interface ILobbiesRepository : IRepository<Lobby>
    {
        Lobby? GetById(int id);

        int GetUniqueId();
    }
}
