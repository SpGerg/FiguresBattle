namespace Server.Models.ChessGames.Datas
{
    using Server.Models.Map.Interfaces;

    public class ChessGame(int id, IMapModel map)
    {
        public int Id => id;

        public IMapModel Map => map;
    }
}
