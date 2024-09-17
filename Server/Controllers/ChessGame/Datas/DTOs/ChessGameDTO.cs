namespace Server.Controllers.ChessGame.Datas.DTOs
{
    using Server.Controllers.Accounts.Datas.DTOs;
    using Server.Models.Abilities.Enums;
    using Server.Models.Figures.Enums;

    public class ChessGameDTO
    {
        public int Id { get; set; }

        public AccountDTO[] Players { get; set; }

        public Dictionary<FigureType, AbilityType[]> FiguresAbilities { get; set; }

        public ChessMoveDTO[] ChessMoves { get; set; }
    }
}
