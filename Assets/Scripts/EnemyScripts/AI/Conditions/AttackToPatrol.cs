using Common;
using UnityEngine;

namespace EnemyScripts.AI.Conditions
{
    public class AttackToPatrol : ICondition
    {
        private bool _inTrigger;
        public AttackToPatrol(TriggerObserver triggerObserver)
        {
            triggerObserver.TriggerEnter += OnTriggerEnter;
            triggerObserver.TriggerExit += OnTriggerExit;
        }

        private void OnTriggerExit(Collider2D obj)
        {
            _inTrigger = true;
        }

        private void OnTriggerEnter(Collider2D obj)
        {
            _inTrigger = false;
        }

        public bool Result()
        {
            return _inTrigger;
        }
        
        
    }
}