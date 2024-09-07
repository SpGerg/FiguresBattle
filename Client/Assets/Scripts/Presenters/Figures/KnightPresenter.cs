namespace Presenters.Figures
{
    using Models.Figures;
    using Views.Figures;

    public class KnightPresenter : FigurePresenter
    {
        public void Awake()
        {
            var model = new BasicFigureModel(this);
            var view = new BasicFigureView(this);

            base.Initialize(model, view);
        }
    }
}
