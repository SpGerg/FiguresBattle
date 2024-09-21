using UnityEngine;
using UnityEngine.Events;

namespace Presenters.Figures
{
    using Enums;
    using Models.Figures.Enums;
    using Models.Figures.Interfaces;
    using Presenters.ChessBoard;
    using Presenters.Figures.Interfaces;
    using Views.Figures.Interfaces;

    public abstract class FigurePresenter : PresenterBase, IFigurePresenter
    {
        [SerializeField]
        private FigureType _figureType;

        [SerializeField]
        private GameObject _white;

        [SerializeField]
        private GameObject _black;

        private SideType _side;

        public new IFigureModel Model { get; }

        public new IFigureView View { get; }

        public UnityEvent Moved { get; } = new();

        public UnityEvent<IFigurePresenter> Kill { get; } = new();

        public SideType Side
        {
            get
            {
                return _side;
            }
            set
            {
                White.SetActive(value is SideType.White);
                Black.SetActive(value is SideType.Black);

                _side = value;
            }
        }

        public GameObject White => _white;

        public GameObject Black => _black;

        public FigureType Type => _figureType;

        public void MoveTo(ChessBoardSquarePresenter presenter)
        {
            Model.MoveTo(presenter);
        }
    }
}
