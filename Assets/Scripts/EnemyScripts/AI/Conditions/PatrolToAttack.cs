using Common;
using UnityEngine;

namespace EnemyScripts.AI.Conditions
{
    public class PatrolToAttack : ICondition
    {
        private bool _inTrigger;
        public PatrolToAttack(TriggerObserver triggerObserver)
        {
            triggerObserver.TriggerEnter += OnTriggerEnter;
            triggerObserver.TriggerExit += OnTriggerExit;
        }

        private void OnTriggerExit(Collider2D obj)
        {
            _inTrigger = false;
        }

        private void OnTriggerEnter(Collider2D obj)
        {
            _inTrigger = true;
        }

        public bool Result()
        {
            return _inTrigger;
        }
        
        
    }
}