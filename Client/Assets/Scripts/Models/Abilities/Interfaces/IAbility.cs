using Models.Abilities.ScriptableObjects;

namespace Models.Abilities.Interfaces
{
    public interface IAbility
    {
        AbilityData Data { get; }

        IAbilityExecutor Executor { get; }
    }
}
