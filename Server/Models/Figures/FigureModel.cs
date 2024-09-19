namespace Server.Models.Figures
{
    using Server.Models.ChessGames.Datas;
    using Server.Models.Enums;
    using Server.Models.Figures.Datas;
    using Server.Models.Figures.Enums;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;
    using Server.Models.Map.Interfaces;
    using Server.Services.Accounts.Datas;

    public abstract class FigureModel(Account user, SideType side, ChessGame chessGame) : IFigureModel
    {
        public Account User => user;

        public SideType Side => side;

        public abstract FigureType FigureType { get; }

        public abstract Direction[] Directions { get; }

        private IChessBoardModel Map => chessGame.Map;

        public bool MoveToIfCan(Vector2Int vector2)
        {
            if (!Map.IsCanMoveTo(Directions, vector2))
            {
                return false;
            }

            Map.SetFigure(this, vector2);
            return true;
        }
    }
}
