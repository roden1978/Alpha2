using Common;
using UnityEngine;

namespace EnemyScripts.AI.Conditions
{
    public class PatrolToNothing : ICondition
    {
        private bool _inTrigger;
        public PatrolToNothing(TriggerObserver triggerObserver)
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