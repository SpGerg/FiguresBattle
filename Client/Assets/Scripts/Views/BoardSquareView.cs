namespace Views
{
    using Views.Figures.Interfaces;

    public class BoardSquareView : ViewBase
    {
        public void MoveFigureToThis(IFigureView view)
        {
            view.MoveTo(this);
        }
    }
}
