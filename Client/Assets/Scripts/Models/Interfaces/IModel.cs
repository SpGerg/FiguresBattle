namespace Models.Interfaces
{
    using Presenters.Interfaces;

    public interface IModel
    {
        IPresenter Presenter { get; }

        void Initialize(IPresenter presenter);
    }
}