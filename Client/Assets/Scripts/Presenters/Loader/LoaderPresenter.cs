namespace Presenters
{
    using Models.Loader.Interfaces;

    public class LoaderPresenter : PresenterBase
    {
        public new ILoaderModel Model { get; set; }
    }
}
