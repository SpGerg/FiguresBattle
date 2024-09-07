using UnityEngine;

namespace Models.Abilities
{
    using Models.Abilities.Features;
    using Models.Abilities.Interfaces;

    public abstract class ActiveAbility : AbilityModel, IAbilityCommand
    {
        public ActiveAbility(IAbilityExecutor executor) : base(executor)
        {
        }

        public abstract Cooldown Cooldown { get; }

        public void ExecuteWithCooldown()
        {
            if (Cooldown is null)
            {
                Execute();

                return;
            }

            if (!Cooldown.IsReady)
            {
                Debug.Log($"Ability was not executed because is not ready. Cooldown: {Cooldown.CooldownInSeconds}, Remaining time: {Cooldown.RemainingTime}");

                return;
            }

            Cooldown.Use();
            Execute();
        }

        public abstract void Execute();
    }
}
