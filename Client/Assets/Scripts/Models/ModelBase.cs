namespace Models
{
    using Models.Interfaces;
    using Presenters.Interfaces;

    public abstract class ModelBase : IModel
    {
        public IPresenter Presenter { get; private set; }

        public void Initialize(IPresenter presenter)
        {
            Presenter = presenter;
        }
    }
}