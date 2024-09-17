namespace Server.Models.ChessGames.Interfaces
{
    using Server.Models.ChessGames.Datas;
    using Server.Models.Interfaces;

    public interface IChessGamesRepository : IRepository<ChessGame>
    {
        int GetUniqueId();

        ChessGame? GetById(int id);
    }
}
