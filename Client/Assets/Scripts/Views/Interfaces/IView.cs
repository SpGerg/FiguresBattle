namespace Views.Interfaces
{
    using Presenters.Interfaces;

    public interface IView
    {
        IPresenter Presenter { get; }
    }
}
