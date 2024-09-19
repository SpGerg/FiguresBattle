namespace Presenters.ChessBoard
{
    using Models.ChessBoard;
    using Models.ChessBoard.Interfaces;
    using Views.ChessBoard.Interfaces;

    public class BasicChessBoardPresenter : ChessBoardPresenter
    {
        public override IChessBoardModel Model => _basicChessBoardModel;

        public override IChessBoardView View { get; }

        private BasicChessBoardModel _basicChessBoardModel;

        public void Awake()
        {
            _basicChessBoardModel = new BasicChessBoardModel();
        }
    }
}
