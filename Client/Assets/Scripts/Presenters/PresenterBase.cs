using UnityEngine;

namespace Presenters
{
    using Views.Interfaces;
    using Presenters.Interfaces;
    using Models.Interfaces;

    public abstract class PresenterBase : MonoBehaviour, IPresenter
    {
        public IModel Model { get; private set; }
        
        public IView View { get; private set; }

        private IUpdatable _updatable;

        private ITransformable _transformable;

        public virtual void Initialize(IModel model, IView view)
        {
            Model = model;
            View = view;

            if (Model is IUpdatable updatable) 
            {
                _updatable = updatable;
            }

            if (Model is ITransformable transformable)
            {
                _transformable = transformable;
            }
        }

        public void Update()
        {
            _updatable?.Update();
            
            transform.SetPositionAndRotation(_transformable.Position, _transformable.Rotation);
            transform.localScale = _transformable.Scale;
        }

        public void FixedUpdate()
        {
            _updatable?.FixedUpdate();
        }

        public void LateUpdate()
        {
            _updatable?.LateUpdate();
        }
    }
}
