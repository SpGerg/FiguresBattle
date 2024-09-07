namespace Views
{
    using Presenters.Interfaces;
    using Views.Interfaces;

    public abstract class ViewBase : IView
    {
        protected ViewBase(IPresenter presenter)
        {
            Presenter = presenter;
        }

        public IPresenter Presenter { get; private set; }
    }
}
