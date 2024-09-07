using Models.Abilities.Interfaces;

namespace Models.Abilities
{
    public abstract class PassiveAbility : AbilityModel
    {
        public PassiveAbility(IAbilityExecutor executor) : base(executor)
        {
        }
    }
}
