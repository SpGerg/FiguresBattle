namespace Views.Figures.Interfaces
{
    using Presenters.Figures.Interfaces;

    public interface IFigureView
    {
        IFigurePresenter Presenter { get; }

        void MoveTo(BoardSquareView view);
    }
}
