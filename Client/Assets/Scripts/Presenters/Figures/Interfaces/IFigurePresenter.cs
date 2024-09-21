using UnityEngine.Events;

namespace Presenters.Figures.Interfaces
{
    using Models.Figures.Enums;
    using Models.Figures.Interfaces;
    using Presenters.ChessBoard;
    using Presenters.Interfaces;
    using Views.Figures.Interfaces;

    public interface IFigurePresenter : IPresenter
    {
        FigureType Type { get; }

        new IFigureModel Model { get; }

        new IFigureView View { get; }

        UnityEvent Moved { get; }

        UnityEvent<IFigurePresenter> Kill { get; }

        void MoveTo(ChessBoardSquarePresenter presenter);
    }
}
