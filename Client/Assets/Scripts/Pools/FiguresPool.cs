using System.Linq;
using UnityEngine;

namespace Pools
{
    using Models.Figures.Enums;
    using Presenters.Figures;
    using Presenters.Figures.Interfaces;

    public class FiguresPool : MonoPoolBase<IFigurePresenter>
    {
        [SerializeField]
        private FiguresFactory _factory;

        public IFigurePresenter Get(FigureType figureType)
        {
            var entity = pool.FirstOrDefault(figure => figure.Type == figureType);

            if (entity is not null)
            {
                return entity;
            }

            return Create(figureType);
        }

        protected IFigurePresenter Create(FigureType figureType)
        {
            return _factory.Create(figureType);
        }

        protected override IFigurePresenter Create()
        {
            return null;
        }
    }
}
