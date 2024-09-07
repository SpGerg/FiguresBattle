namespace Models.Figures
{
    using Presenters.Figures.Interfaces;

    public class BasicFigureModel : FigureModel
    {
        public BasicFigureModel(IFigurePresenter presenter) : base(presenter)
        {
        }
    }
}
