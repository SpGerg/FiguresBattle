namespace Presenters
{
    using Views.Interfaces;
    using Presenters.Interfaces;
    using Models.Interfaces;

    public abstract class PresenterBase : IPresenter
    {
        public IModel Model { get; private set; }
        
        public IView View { get; private set; }

        public virtual void Initialize(IModel model, IView view)
        {
            Model = model;
            View = view;
        }
    }
}
