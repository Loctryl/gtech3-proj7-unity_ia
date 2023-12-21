using UnityEngine;

namespace _Scripts.BehaviourTree.BossNode
{
    public class TeleportNode : ActionNode
    {
        public float range;
        public string bossSpellKey;
        private BossSpells _spells;
        protected override void OnEnter()
        {
            _spells = (BossSpells)blackBoard.dataContext[bossSpellKey];
            
            _spells.TeleportTo((Random.insideUnitSphere * range) + blackBoard.player.transform.position);
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