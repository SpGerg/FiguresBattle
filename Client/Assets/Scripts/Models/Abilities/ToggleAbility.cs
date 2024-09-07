using Models.Abilities.Interfaces;

namespace Models.Abilities
{
    public abstract class ToggleAbility : AbilityModel, IAbilityCommand
    {
        public ToggleAbility(IAbilityExecutor executor) : base(executor)
        {
        }

        public bool _isToggle;

        public bool IsToggle
        {
            get
            {
                return _isToggle; 
            }
            set
            {
                if (value)
                {
                    Enabled();
                }
                else
                {
                    Disabled();
                }

                _isToggle = value;
            }
        }

        public virtual void Enabled() { }

        public virtual void Disabled() { }

        public void Execute()
        {
            IsToggle = !_isToggle;
        }
    }
}
