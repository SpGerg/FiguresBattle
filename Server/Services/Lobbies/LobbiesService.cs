namespace Server.Services.Lobbies
{
    using Server.Controllers.Accounts.Datas.DTOs;
    using Server.Models.Abilities.Enums;
    using Server.Models.Figures;
    using Server.Models.Figures.Enums;
    using Server.Models.Lobbies.Datas;
    using Server.Models.Lobbies.Interfaces;
    using Server.Services.Accounts;
    using Server.Services.Lobbies.Datas.DTOs;
    using Server.Services.Lobbies.Enums;

    public class LobbiesService(ILogger<LobbiesService> logger, ILobbiesRepository lobbiesRepository, AccountsService accountsService, FiguresFactory figuresFactory)
    {
        public Lobby CreateLobby(string creator)
        {
            if (lobbiesRepository.GetLobbyWithAccount(creator) is not null)
            {
                throw new Exception("You cannot create a lobby while you are in another lobby");
            }

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

        public Task<LobbyActionDTO> WaitForAction(int id)
        {
            var lobby = GetById(id);

            var count = lobby.Accounts.Count;
            var figures = lobby.FiguresAbilities.ToDictionary().Values;

            var account = lobby.Accounts.Last();

            while (!lobby.IsDeleted)
            {
                if (lobby.Accounts.Count != count)
                {
                    var type = count > lobby.Accounts.Count ? LobbyActionType.PlayerLeft : LobbyActionType.PlayerJoin;
                    account = type is LobbyActionType.PlayerLeft ? lobby.Accounts.Last() : account;

                    return Task.FromResult(new LobbyActionDTO()
                    {
                        Type = type,
                        Value = new AccountDTO() 
                        {
                             Username = account.Username
                        }
                    });
                }

                if (!lobby.FiguresAbilities.Values.SequenceEqual(figures))
                {
                    return Task.FromResult(new LobbyActionDTO()
                    {
                        Type = LobbyActionType.FiguresAbilitiesChanged,
                        Value = lobby.FiguresAbilities
                    });
                }
            }

            return Task.FromResult(new LobbyActionDTO()
            {
                Type = LobbyActionType.Deleted
            });
        }

        public async void Join(int id, string username)
        {
            var account = await accountsService.GetUser(username);

            var lobby = GetById(id);

            if (lobby.Accounts.Count + 1 > lobby.MaxPlayers)
            {
                throw new Exception($"You cant join to this lobby.");
            }

            lobby.Accounts.Add(account);
        }

        public async void Leave(int id, string username)
        {
            var account = await accountsService.GetUser(username);

            var lobby = GetById(id);

            lobby.Accounts.Remove(account);

            if (lobby.Accounts.Count > 0)
            {
                return;
            }

            lobbiesRepository.Remove(lobby);
        }

        public Lobby GetById(int id)
        {
            var lobby = lobbiesRepository.GetById(id);

            if (lobby is null)
            {
                throw new Exception($"Unknown lobby with {id} id");
            }

            return lobby;
        }
    }
}
