using Common;
using UnityEngine;

namespace EnemyScripts.AI.Conditions
{
    public class ChaseDistance : ICondition
    {
        private readonly Component _target;
        private readonly Component _origin;
        private readonly float _distance;

        public ChaseDistance(Component target, Component origin, float distance)
        {
            _target = target;
            _origin = origin;
            _distance = distance;
        }

        public bool Result()
        {
            return Vector2.Distance(_target.transform.position, _origin.transform.position) < _distance;
        }
    }
}
