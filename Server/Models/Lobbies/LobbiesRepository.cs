using System.Collections.Concurrent;

namespace Server.Models.Lobbies
{
    using Server.Models.Lobbies.Datas;
    using Server.Models.Lobbies.Interfaces;

    public class LobbiesRepository(ILogger logger) : ILobbiesRepository
    {
        private readonly ConcurrentDictionary<int, Lobby> _lobbies = [];

        public ICollection<Lobby> Entities => _lobbies.Values;

        public void Add(Lobby entity)
        {
            if (_lobbies.ContainsKey(entity.Id)) 
            {
                logger.LogError($"Lobby with {entity.Id} already exists.");

                return;
            }

            _lobbies.GetOrAdd(entity.Id, entity);
        }

        public Lobby? GetById(int id)
        {
            if (!_lobbies.TryGetValue(id, out var entity))
            {
                return null;
            }

            return entity;
        }

        public int GetUniqueId()
        {
            if (_lobbies.IsEmpty)
            {
                return 1;
            }

            var ordered = _lobbies.OrderBy(lobby => lobby.Key);

            return ordered.FirstOrDefault().Key + 1;
        }

        public bool Remove(Lobby entity)
        {
            if (!_lobbies.TryRemove(new KeyValuePair<int, Lobby>(entity.Id, entity)))
            {
                return false;
            } 

            entity.IsDeleted = true;

            return true;
        }
    }
}