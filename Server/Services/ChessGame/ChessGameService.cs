namespace Server.Services.Map
{
    using Server.Controllers.ChessGame.Datas.DTOs;
    using Server.Models.ChessGames.Datas;
    using Server.Models.ChessGames.Interfaces;
    using Server.Models.Map.Datas;

    public class ChessGameService(IChessGamesRepository chessGamesRepository)
    {
        public void SetNewPositions(int gameId, Vector2Int[] oldPositions, Vector2Int[] newPositions)
        {
            var chessGame = GetChessGame(gameId);

            chessGame.Map.MoveTo(oldPositions, newPositions);
        }

        public Task<ChessMoveDTO[]> WaitForChessMove(int id)
        {
            return WaitForChessMove(GetChessGame(id));
        }

        public Task<ChessMoveDTO[]> WaitForChessMove(ChessGame chessGame)
        {
            var map = chessGame.Map;

            var last = map.ChessMoveCount;

            while (last == map.ChessMoveCount)
            {
                continue;
            }

            return Task.FromResult(map.ChessMoves[^1]);
        }

        public ChessGame GetChessGame(int id)
        {
            var chessGame = chessGamesRepository.GetById(id);

            if (chessGame is null)
            {
                throw new Exception($"Unknown game with {id} id");
            }

            return chessGame;
        }
    }
}
