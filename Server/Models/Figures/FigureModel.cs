namespace Server.Models.Figures
{
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;
    using Server.Services.Accounts.Datas;
    using Server.Services.Map;

    public abstract class FigureModel(Account user, SideType side, ChessGameService chessGameService) : IFigureModel
    {
        public Account User => user;

        public SideType Side => side;

        public abstract FigureType FigureType { get; }

        public void MoveTo(Vector2Int vector2)
        {
            chessGameService.SetFigure(this, vector2);
        }
    }
}
