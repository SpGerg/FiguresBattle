using System;
using UnityEngine;

namespace Views.Figures
{
    using Enums;
    using Views.Figures.Interfaces;
    using Presenters.Figures.Interfaces;

    public abstract class FigureView : ViewBase, IFigureView
    {
        [Serializable]
        public class DirectionAndValue
        {
            [SerializeField]
            private DirectionType _direction;

            [SerializeField]
            private float _value;

            [SerializeField]
            private bool _isInfinity;

            [SerializeField]
            private bool _isEnemyRequired;

            public DirectionType Direction { get => _direction; }

            public float Value { get => _value; }

            public bool IsInfinity { get => _isInfinity; }

            public bool IsEnemyRequired { get => _isEnemyRequired; }
        }

        [SerializeField]
        private GameObject _white;

        [SerializeField]
        private GameObject _black;

        [SerializeField]
        private DirectionAndValue[] _directions;

        private SideType _side;

        public new IFigurePresenter Presenter { get; private set; }

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

        public virtual void Initialize(IFigurePresenter presenter)
        {
            Presenter = presenter;
        }

        public void MoveTo(BoardSquareView view)
        {
            Presenter.MoveTo(view);
        }
    }
}
