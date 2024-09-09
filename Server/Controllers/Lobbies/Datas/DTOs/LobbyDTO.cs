namespace Server.Controllers.Lobbies.Datas.DTOs
{
    using Server.Models.Abilities.Enums;
    using Server.Models.Figures.Enums;
    using Server.Services.Accounts.Datas;

    public class LobbyDTO
    {
        public int Id { get; set; }

        public int MaxPlayers { get; set; }

        public Dictionary<FigureType, AbilityType[]>? FiguresAbilities { get; set; }

        public List<Account>? Users { get; set; }
    }
}
