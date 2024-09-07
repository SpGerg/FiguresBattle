using UnityEngine;

namespace Models.Abilities
{
    using Models.Abilities.Interfaces;
    using Models.Abilities.ScriptableObjects;
    using Models.Figures.Interfaces;

    public abstract class AbilityModel : ModelBase, IAbility
    {
        public AbilityModel(IAbilityExecutor executor)
        {
            Executor = executor;

            if (executor is IFigureModel model)
            {
                FigureExecutor = model;
            }
            else
            {
                Debug.LogWarning($"Exeuctor is null or not figure. {executor}");
            }
        }

        public AbilityData Data { get; }

        public IAbilityExecutor Executor { get; }

        public IFigureModel FigureExecutor { get; }

        public virtual void Equip() { }

        public virtual void Unequip() { }
    }
}
