namespace Views
{
    using Presenters;
    using Views.Figures.Interfaces;

    public class BoardSquareView : ViewBase
    {
        public BoardSquareView(BoardSquarePresenter presenter) : base(presenter)
        {
        }

        public void MoveFigureToThis(IFigureView view)
        {
            view.MoveTo(this);
        }
    }
}
