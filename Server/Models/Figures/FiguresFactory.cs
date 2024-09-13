namespace Server.Models.Figures
{
    using Server.Models.Abilities.Enums;
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Models.Figures.Interfaces;
    using Server.Services.Accounts.Datas;
    using Server.Services.Map;

    public class FiguresFactory(ChessGameService chessGameService)
    {
        public IFigureModel? Create(FigureType figureType, Account user, SideType sideType)
        {
            return figureType switch
            {
                FigureType.Knight => new KnightModel(user, sideType, chessGameService),
                _ => null,
            };
        }

        public AbilityType[]? GetDefaultAbilities(FigureType figureType)
        {
            return figureType switch
            {
                FigureType.Knight => [.. KnightModel.DefaultAbilities],
                _ => null
            };
        }
    }
}
