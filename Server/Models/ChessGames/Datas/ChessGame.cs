namespace Server.Models.ChessGames.Datas
{
    using Server.Models.Map.Interfaces;

    public class ChessGame(int id, IChessBoardModel map)
    {
        public int Id => id;

        public IChessBoardModel Map => map;
    }
}
