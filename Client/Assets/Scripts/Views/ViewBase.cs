using UnityEngine;

namespace Views
{
    using Presenters.Interfaces;
    using Views.Interfaces;

    public abstract class ViewBase : MonoBehaviour, IView
    {
        public IPresenter Presenter { get; private set; }

        public void Initialize(IPresenter presenter)
        {
            Presenter = presenter;
        }
    }
}
