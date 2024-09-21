using UnityEngine;

namespace Presenters.ChessBoard
{
    using Datas;
    using Models.ChessBoard;
    using Models.ChessBoard.Interfaces;
    using Pools;
    using System.Collections.Generic;
    using System.Linq;
    using Views.ChessBoard.Interfaces;

    public class BasicChessBoardPresenter : ChessBoardPresenter
    {
        [SerializeField]
        private FiguresPool _figuresPool;

        [SerializeField]
        private KeyAndValue<Vector2Int, ChessBoardSquarePresenter>[] _chessBoardSquares;

        private BasicChessBoardModel _basicChessBoardModel;

        public override IChessBoardModel Model => _basicChessBoardModel;

        public override IChessBoardView View { get; }

        public void Awake()
        {
            var dictionary  = new Dictionary<Vector2Int, ChessBoardSquarePresenter>();

            foreach (var keyAndValue in _chessBoardSquares)
            {
                dictionary.Add(keyAndValue.Key, keyAndValue.Value);
            }

            _basicChessBoardModel = new BasicChessBoardModel(_figuresPool, dictionary);
        }
    }
}
