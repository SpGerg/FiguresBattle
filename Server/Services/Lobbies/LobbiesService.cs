namespace Server.Services.Lobbies
{
    using Server.Models.Abilities.Enums;
    using Server.Models.Figures;
    using Server.Models.Figures.Enums;
    using Server.Models.Lobbies.Datas;
    using Server.Models.Lobbies.Interfaces;
    using Server.Services.Accounts;

    public class LobbiesService(ILogger<LobbiesService> logger, ILobbiesRepository lobbiesRepository, AccountsService accountsService, FiguresFactory figuresFactory)
    {
        public Lobby CreateLobby()
        {
            var figures = Enum.GetValues(typeof(FigureType));
            var figuresAbilities = new Dictionary<FigureType, AbilityType[]>();

            foreach (FigureType figure in figures)
            {
                var abilities = figuresFactory.GetDefaultAbilities(figure);

                if (abilities is null)
                {
                    logger.LogWarning($"Figure with {figure} name, doesnt have default abilities.");

                    abilities = [];
                }

                figuresAbilities.Add(figure, abilities);
            }

            var lobby = new Lobby(lobbiesRepository.GetUniqueId(), figuresAbilities);
            lobbiesRepository.Add(lobby);

            return lobby;
        }

        public async void Join(string username, int id)
        {
            var account = await accountsService.GetUser(username) ?? throw new Exception($"Unknown account with {username} name.");

            var lobby = lobbiesRepository.Get(id) ?? throw new Exception($"Unknown lobby with {id} id.");

            if (lobby.Accounts.Count + 1 > lobby.MaxPlayers)
            {
                throw new Exception($"You cant join to this lobby.");
            }

            lobby.Accounts.Add(account);
        }
    }
}
