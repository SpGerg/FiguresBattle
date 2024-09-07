using UnityEngine.Events;

namespace Presenters.Figures.Interfaces
{
    using Models.Figures.Interfaces;
    using Presenters.Interfaces;
    using Views;
    using Views.Figures.Interfaces;

    public interface IFigurePresenter : IPresenter
    {
        new IFigureModel Model { get; }

        new IFigureView View { get; }

        UnityEvent Moved { get; }

        UnityEvent<IFigurePresenter> Kill { get; }

        void MoveTo(BoardSquareView view);
    }
}
