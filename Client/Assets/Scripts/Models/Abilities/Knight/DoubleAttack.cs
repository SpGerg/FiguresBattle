namespace Models.Abilities.Knight
{
    using Models.Abilities.Interfaces;
    using Presenters.Figures.Interfaces;

    public class DoubleAttack : PassiveAbility
    {
        public DoubleAttack(IAbilityExecutor executor) : base(executor)
        {
        }

        private int _count;

        public override void Equip()
        {
            FigureExecutor.Presenter.Kill.AddListener(MoveAndKillAgainOnKill);

            base.Equip();
        }

        public override void Unequip()
        {
            FigureExecutor.Presenter.Kill.RemoveListener(MoveAndKillAgainOnKill);

            base.Unequip();
        }

        public void MoveAndKillAgainOnKill(IFigurePresenter presenter)
        {
            
        }
    }
}