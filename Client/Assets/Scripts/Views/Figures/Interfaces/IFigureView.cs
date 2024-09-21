namespace Views.Figures.Interfaces
{
    using Presenters.ChessBoard;
    using Presenters.Figures.Interfaces;

    public interface IFigureView
    {
        IFigurePresenter Presenter { get; }

        void MoveTo(ChessBoardSquarePresenter presenter);
    }
}
