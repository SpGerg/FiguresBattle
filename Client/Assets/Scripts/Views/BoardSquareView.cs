using UnityEngine;

namespace Views
{
    using Views.Figures.Interfaces;

    public class BoardSquareView : ViewBase
    {
        public Vector2 FigurePosition { get; private set; }

        public void Awake()
        {
            FigurePosition = transform.position + new Vector3(0, 2, 0);
        }

        public void MoveFigureToThis(IFigureView view)
        {
            view.MoveTo(this);
        }
    }
}
