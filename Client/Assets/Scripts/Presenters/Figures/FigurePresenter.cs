using System;
using UnityEngine;
using UnityEngine.Events;

namespace Presenters.Figures
{
    using Enums;
    using Models.Figures.Interfaces;
    using Presenters.Figures.Datas;
    using Presenters.Figures.Interfaces;
    using Views;
    using Views.Figures.Interfaces;

    public abstract class FigurePresenter : PresenterBase, IFigurePresenter
    {
        [SerializeField]
        private GameObject _white;

        [SerializeField]
        private GameObject _black;

        [SerializeField]
        private DirectionAndValue[] _directions;

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

        public void MoveTo(BoardSquareView view)
        {
            Model.MoveTo(view);
        }
    }
}
