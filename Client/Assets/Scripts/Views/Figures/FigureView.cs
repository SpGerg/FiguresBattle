namespace Views.Figures
{
    using Views.Figures.Interfaces;
    using Presenters.Figures.Interfaces;
    using Presenters.ChessBoard;

    public abstract class FigureView : ViewBase, IFigureView
    {
        protected FigureView(IFigurePresenter presenter) : base(presenter)
        {
        }

        public new IFigurePresenter Presenter { get; private set; }

        public void MoveTo(ChessBoardSquarePresenter presenter)
        {
            Presenter.MoveTo(presenter);
        }
    }
}
