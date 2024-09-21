namespace Models.Figures
{
    using Models.Figures.Interfaces;
    using Presenters.ChessBoard;
    using Presenters.Figures.Interfaces;

    public abstract class FigureModel : TransformableModel, IFigureModel
    {
        protected FigureModel(IFigurePresenter presenter)
        {
            Presenter = presenter;
        }

        public new IFigurePresenter Presenter { get; }

        public void MoveTo(ChessBoardSquarePresenter presenter)
        {
            presenter.Figure = Presenter;
        }
    }
}
