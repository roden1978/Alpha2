using Common;
using UnityEngine;

namespace EnemyScripts.AI.Conditions
{
    public class NothingToPatrol : ICondition
    {
        private bool _inTrigger;
        public NothingToPatrol(TriggerObserver triggerObserver)
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