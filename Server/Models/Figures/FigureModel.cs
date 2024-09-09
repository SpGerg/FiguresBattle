namespace Server.Models.Figures
{
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;
    using Server.Models.Map.Interfaces;
    using Server.Services.Accounts.Datas;

    public abstract class FigureModel(Account user, SideType side, IMapModel map) : IFigureModel
    {
        public Account User => user;

        public SideType Side => side;

        public IMapModel Map => map;

        public abstract FigureType FigureType { get; }

        public void MoveTo(Vector2Int vector2)
        {
            map.SetFigure(this, vector2);
        }
    }
}
