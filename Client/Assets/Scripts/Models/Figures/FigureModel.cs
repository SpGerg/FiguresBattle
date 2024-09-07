namespace Models.Figures
{
    using Models.Abilities.Interfaces;
    using Models.Figures.Interfaces;
    using Presenters.Figures.Interfaces;
    using Views;

    public abstract class FigureModel : TransformableModel, IFigureModel, IAbilityExecutor
    {
        protected FigureModel(IFigurePresenter presenter)
        {
            Presenter = presenter;
        }

        public new IFigurePresenter Presenter { get; }

        public void MoveTo(BoardSquareView view)
        {
            Position = view.FigurePosition;
        }
    }
}
