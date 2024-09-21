using System.Collections.Generic;

namespace Models.ChessBoard
{
    using Datas;
    using Models.ChessBoard.Interfaces;
    using Models.Figures.Enums;
    using Pools;
    using Presenters.ChessBoard;

    public class BasicChessBoardModel : IChessBoardModel
    {
        public BasicChessBoardModel(FiguresPool figuresPool, IReadOnlyDictionary<Vector2Int, ChessBoardSquarePresenter> chessBoardSquares)
        {
            _figuresPool = figuresPool;
            _chessBoardSquares = chessBoardSquares;
        }

        private readonly FiguresPool _figuresPool;

        private readonly IReadOnlyDictionary<Vector2Int, ChessBoardSquarePresenter> _chessBoardSquares;

        public void SetFigure(FigureType figureType, Vector2Int vector2Int)
        {
            if (!_chessBoardSquares.TryGetValue(vector2Int, out var chessBoardSquare))
            {
                UnityEngine.Debug.LogError($"Unknown {vector2Int} position");

                return;
            }

            _chessBoardSquares[vector2Int].Figure = _figuresPool.Get(figureType);
        }
    }
}
