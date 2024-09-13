namespace Server.Models.Figures
{
    using Server.Models.Abilities.Enums;
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Services.Accounts.Datas;
    using Server.Services.Map;

    public class KnightModel(Account user, SideType side, ChessGameService chessGameService) : FigureModel(user, side, chessGameService)
    {
        public static IReadOnlyList<AbilityType> DefaultAbilities { get; } = [ AbilityType.DoubleAttack ];

        public override FigureType FigureType => FigureType.Knight;
    }
}
