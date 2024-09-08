using System.Collections.Concurrent;

namespace Server.Services.Lobbies
{
    using Server.Services.Accounts.Datas;
    using Server.Services.Lobbies.Datas;

    public class LobbiesService
    {
        private readonly ConcurrentBag<Lobby> _lobbies = [];

        public Lobby CreateLobby(User creator)
        {
            _lobbies.Add(new Lobby())
        }
    }
}
