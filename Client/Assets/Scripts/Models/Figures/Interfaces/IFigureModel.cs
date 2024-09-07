namespace Models.Figures.Interfaces
{
    using Models.Abilities.Interfaces;
    using Presenters.Figures.Interfaces;
    using Views;

    public interface IFigureModel : IAbilityExecutor
    {
        IFigurePresenter Presenter { get; }

        void MoveTo(BoardSquareView view);
    }
}
