using Common;
using EnemyScripts.AI.States;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts.AI
{
    public class OctonoidAI : EnemyAI
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private GameObject _model;
        [Header("Patrol")] [SerializeField] private float _patrolSpeed;
        [SerializeField] private int _patrolDistance;

        public override void Construct(Player player)
        {
            Player = player;
            StateMachine = new StateMachine();

            IState groundTwoWayPointsPatrol = new GroundTwoWayPointsPatrol(transform, _rigidbody,
                _model, _patrolDistance, _patrolSpeed);
            StateMachine.SetState(groundTwoWayPointsPatrol);
        }
        
        protected override void Update()
        {
            StateMachine.Update();
        }
    }
}