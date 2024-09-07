using UnityEngine.Events;

namespace Presenters.Figures
{
    using Models.Figures.Interfaces;
    using Presenters.Figures.Interfaces;
    using Views;
    using Views.Figures.Interfaces;

    public abstract class FigurePresenter : PresenterBase, IFigurePresenter
    {
        public new IFigureModel Model { get; }

        public new IFigureView View { get; }

        public UnityEvent Moved { get; } = new();

        public UnityEvent<IFigurePresenter> Kill { get; } = new();

        public void MoveTo(BoardSquareView view)
        {
            Model.MoveTo(view);
        }
    }
}
