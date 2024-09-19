using System;

namespace Presenters.ChessBoard
{
    using Models.Interfaces;
    using Views;
    using Views.Interfaces;

    public class ChessBoardSquarePresenter : PresenterBase
    {
        public new BoardSquareView View { get; private set; }

        public void Initialize(BoardSquareView view)
        {
            View = view;

            base.Initialize(null, view);
        }

        public override void Initialize(IModel model, IView view)
        {
            if (view is not BoardSquareView boardSquareView)
            {
                throw new ArgumentException($"Except {nameof(BoardSquareView)}");
            }

            Initialize(boardSquareView);
        }
    }
}
