namespace Server.Models.Figures
{
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Models.Figures.Interfaces;
    using Server.Services.Accounts.Datas;

    public class FiguresFactory
    {
        public IFigureModel? Create(FigureType figureType, User user, SideType sideType)
        {
            return figureType switch
            {
                FigureType.Knight => new KnightModel(user, sideType),
                _ => null,
            };
        }
    }
}
