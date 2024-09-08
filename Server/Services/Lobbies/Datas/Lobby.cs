namespace Server.Services.Lobbies.Datas
{
    using Server.Models.Abilities.Enums;
    using Server.Models.Figures.Enums;
    using Server.Services.Accounts.Datas;

    public class Lobby
    {
        public Lobby()
        {
            Users = new List<User>(MaxPlayers);

            foreach (var user in Users)
            {

            }
        }

        public int MaxPlayers { get; init; }

        public Dictionary<FigureType, AbilityType[]> FiguresAbilities { get; }

        public List<User> Users { get; }
    }
}
