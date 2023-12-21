namespace _Scripts.BehaviourTree.BossNode
{
    public class SpikeArmorNode : ActionNode
    {
        public string bossSpellKey;
        private BossSpells _spells;
        
        protected override void OnEnter()
        {
            _spells = (BossSpells)blackBoard.dataContext[bossSpellKey];
            _spells.CastMeleeAoE();
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

        protected override void OnExit()
        {
        }
    }
}