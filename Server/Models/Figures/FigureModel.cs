namespace Server.Models.Figures
{
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;
    using Server.Services.Accounts.Datas;

    public abstract class FigureModel(User user, SideType side) : IFigureModel
    {
        public User User => user;

        public SideType Side => side;

        public abstract FigureType FigureType { get; }

        public void MoveTo(Vector2Int vector2)
        {
            
        }
    }
}
