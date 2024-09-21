namespace Models.Figures.Interfaces
{
    using Presenters.ChessBoard;
    using Presenters.Figures.Interfaces;

    public interface IFigureModel
    {
        IFigurePresenter Presenter { get; }

        void MoveTo(ChessBoardSquarePresenter presenter);
    }
}
