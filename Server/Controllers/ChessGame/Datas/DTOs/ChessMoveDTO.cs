namespace Server.Controllers.ChessGame.Datas.DTOs
{
    using Server.Models.Map.Datas;

    public class ChessMoveDTO
    {
        public Vector2Int OldPosition { get; set; }

        public Vector2Int NewPosition { get; set; }
    }
}
