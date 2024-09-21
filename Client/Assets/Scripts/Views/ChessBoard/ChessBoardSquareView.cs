namespace Views.ChessBoard
{
    using Presenters.ChessBoard;
    using Views.Figures.Interfaces;

    public class ChessBoardSquareView : ViewBase
    {
        public ChessBoardSquareView(ChessBoardSquarePresenter presenter) : base(presenter)
        {
            _presenter = presenter;
        }

        private readonly ChessBoardSquarePresenter _presenter;

        public void MoveFigureToThis(IFigureView view)
        {
            view.MoveTo(_presenter);
        }

        public void PlayFigureEnterEffect()
        {

        }
    }
}
