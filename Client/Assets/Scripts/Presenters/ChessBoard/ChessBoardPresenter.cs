using System.Collections.Generic;
using UnityEngine;

namespace Presenters.ChessBoard
{
    using Datas;
    using Models.ChessBoard.Interfaces;
    using Models.Figures.Enums;
    using Views.ChessBoard.Interfaces;

    public abstract class ChessBoardPresenter : PresenterBase
    {
        public abstract new IChessBoardModel Model { get; }

        public abstract new IChessBoardView View { get; }

        [SerializeField]
        private ChessBoardSquarePresenter[] _boardSquares;

        public ChessBoardSquarePresenter[] BoardSquares => _boardSquares;

        public void SetFigures(IReadOnlyDictionary<FigureType, Vector2Int> black, IReadOnlyDictionary<FigureType, Vector2Int> white)
        {
            foreach (var keyValuePair in black) 
            {
                Model.SetFigure(keyValuePair.Key, keyValuePair.Value);
            }

            foreach (var keyValuePair in white)
            {
                Model.SetFigure(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}
