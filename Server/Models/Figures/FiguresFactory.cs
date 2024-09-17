namespace Server.Models.Figures
{
    using Server.Models.Abilities.Enums;
    using Server.Models.ChessGames.Datas;
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Models.Figures.Interfaces;
    using Server.Services.Accounts.Datas;

    public class FiguresFactory()
    {
        public IFigureModel? Create(FigureType figureType, Account user, SideType sideType, ChessGame chessGame)
        {
            return figureType switch
            {
                FigureType.Knight => new KnightModel(user, sideType, chessGame),
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
