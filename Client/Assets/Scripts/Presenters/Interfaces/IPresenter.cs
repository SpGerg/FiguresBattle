namespace Presenters.Interfaces
{
    using Models.Interfaces;
    using Views.Interfaces;

    public interface IPresenter
    {
        IModel Model { get; }

        IView View { get; }

        void Initialize(IModel model, IView view);
    }
}
