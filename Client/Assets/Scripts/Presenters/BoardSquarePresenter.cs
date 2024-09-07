using System;

namespace Presenters
{
    using Models.Interfaces;
    using Views;
    using Views.Interfaces;

    public class BoardSquarePresenter : PresenterBase
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
