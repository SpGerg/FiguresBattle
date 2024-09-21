using System;
using UnityEngine;

namespace Presenters.ChessBoard
{
    using Models.Interfaces;
    using Presenters.Figures.Interfaces;
    using Views.ChessBoard;
    using Views.Interfaces;

    public class ChessBoardSquarePresenter : PresenterBase
    {
        [SerializeField]
        private ChessBoardPresenter _chessBoardPresenter;

        private IFigurePresenter _figurePresenter;

        public new ChessBoardSquareView View { get; private set; }

        public IFigurePresenter Figure
        {
            get
            {
                return _figurePresenter;
            }
            set
            {
                _figurePresenter = value;

                if (value is not null)
                {
                    View.PlayFigureEnterEffect();
                }
            }
        }

        public void Initialize(ChessBoardSquareView view)
        {
            View = view;

            base.Initialize(null, view);
        }

        public override void Initialize(IModel model, IView view)
        {
            if (view is not ChessBoardSquareView boardSquareView)
            {
                throw new ArgumentException($"Except {nameof(ChessBoardSquareView)}");
            }

            Initialize(boardSquareView);
        }
    }
}
