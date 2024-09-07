namespace Views.Figures
{
    using Models.Figures;
    using Presenters.Figures;

    public class KnightView : FigureView
    {
        public void Awake()
        {
            var presenter = new BasicFigurePresenter();
            var model = new BasicFigureModel(presenter);

            presenter.Initialize(model, this);
        }
    }
}
