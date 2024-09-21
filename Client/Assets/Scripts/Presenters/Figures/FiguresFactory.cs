using System.Linq;
using UnityEngine;

namespace Presenters.Figures
{
    using Datas;
    using Models.Figures.Enums;
    using Presenters.Figures.Interfaces;

    public class FiguresFactory : MonoBehaviour
    {
        [SerializeField]
        private KeyAndValue<FigureType, FigurePresenter>[] _templates;

        public IFigurePresenter Create(FigureType figureType)
        {
            return Instantiate(_templates.FirstOrDefault(template => template.Key == figureType).Value);
        }
    }
}
