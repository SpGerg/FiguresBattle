using System.Collections.Concurrent;

namespace Server.Models.ChessGames
{
    using Server.Models.ChessGames.Datas;
    using Server.Models.ChessGames.Interfaces;

    public class ChessGamesRepository : IChessGamesRepository
    {
        public ICollection<ChessGame> Entities => _chessGames.Values;

        private readonly ConcurrentDictionary<int, ChessGame> _chessGames = [];

        public void Add(ChessGame entity)
        {
            _chessGames.GetOrAdd(GetUniqueId(), entity);
        }

        public bool Remove(ChessGame entity)
        {
            return _chessGames.TryRemove(new KeyValuePair<int, ChessGame>(entity.Id, entity));
        }

        public int GetUniqueId()
        {
            if (_chessGames.IsEmpty)
            {
                return 1;
            }

            var ordered = _chessGames.OrderBy(lobby => lobby.Key);

            return ordered.FirstOrDefault().Key + 1;
        }

        public ChessGame GetById(int id)
        {
            if (!_chessGames.TryGetValue(id, out var value))
            {
                return null;
            }

            return value;
        }
    }
}