using UnityEngine.Events;

namespace Presenters.Figures.Interfaces
{
    using Models.Figures.Interfaces;
    using Views;
    using Views.Figures.Interfaces;

    public interface IFigurePresenter
    {
        IFigureModel Model { get; }

        IFigureView View { get; }

        UnityEvent Moved { get; }

        UnityEvent<IFigurePresenter> Kill { get; }

        void MoveTo(BoardSquareView view);
    }
}
