namespace Server.Models.Figures
{
    using Server.Models.Abilities.Enums;
    using Server.Models.ChessGames.Datas;
    using Server.Models.Enums;
    using Server.Models.Figures.Datas;
    using Server.Models.Figures.Enums;
    using Server.Models.Map.Datas;
    using Server.Services.Accounts.Datas;
    using Server.Services.Map;

    public class KnightModel(Account user, SideType side, ChessGame chessGame) : FigureModel(user, side, chessGame)
    {
        public static IReadOnlyList<AbilityType> DefaultAbilities { get; } = [AbilityType.DoubleAttack];

        public override FigureType FigureType => FigureType.Knight;

        public override Direction[] Directions { get; } =
        [
            //00#0
            //0000
            //0@00
            new()
            {
                Position = new Vector2Int(1, 3),
                IsCanKill = true
            },
            //0000
            //000#
            //0@00
            new()
            {
                Position = new Vector2Int(2, 1),
                IsCanKill = true
            },
            //0#00
            //0000
            //00@0
            new()
            {
                Position = new Vector2Int(-1, 3),
                IsCanKill = true
            },
            //0000
            //#000
            //00@0
            new()
            {
                Position = new Vector2Int(-2, 1),
                IsCanKill = true
            },
            //00@0
            //0000
            //0#00
            new()
            {
                Position = new Vector2Int(2, -2),
                IsCanKill = true
            },
            //00@0
            //#000
            //0000
            new()
            {
                Position = new Vector2Int(2, -1),
                IsCanKill = true
            },
            //0@00
            //0000
            //00#0
            new()
            {
                Position = new Vector2Int(-1, -3),
                IsCanKill = true
            },
            //0@00
            //000#
            //0000
            new()
            {
                Position = new Vector2Int(-2, 1),
                IsCanKill = true
            }
        ];
    }
}
