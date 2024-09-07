namespace Presenters.Figures
{
    using Models.Figures;
    using Models.Interfaces;
    using Views.Interfaces;

    public class BasicFigurePresenter : FigurePresenter
    {
        public new BasicFigureModel Model { get; private set; }

        public override void Initialize(IModel model, IView view)
        {
            if (model is BasicFigureModel basicFigureModel)
            {
                Model = basicFigureModel;
            }

            base.Initialize(model, view);
        }
    }
}
