namespace Server.Models.Lobbies.Datas
{
    using Server.Models.Abilities.Enums;
    using Server.Models.Figures.Enums;
    using Server.Services.Accounts.Datas;

    public class Lobby
    {
        public Lobby(int id, Dictionary<FigureType, AbilityType[]> figuresAbilities)
        {
            Id = id;
            Accounts = new List<Account>(MaxPlayers);
            FiguresAbilities = figuresAbilities;
        }

        public int Id { get; }

        public int MaxPlayers { get; init; }

        public Dictionary<FigureType, AbilityType[]> FiguresAbilities { get; } = [];

        public List<Account> Accounts { get; }
    }
}
