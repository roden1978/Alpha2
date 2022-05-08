using Common;
using EnemyScripts.AI.Conditions;
using EnemyScripts.AI.States;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

namespace EnemyScripts.AI
{
    public class SpiderAI : EnemyAI
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private GameObject _model;
        [SerializeField] private TriggerObserver _triggerObserver;
        [Header("Patrol")] [SerializeField] private float _patrolSpeed;
        [SerializeField] private int _patrolDistance;
        
        public override void Construct(Player player)
        {
            Player = player;
            StateMachine = new StateMachine();
            IState groundTwoWayPointsPatrol = new GroundTwoWayPointsPatrol(transform, _rigidbody,
                _model, _patrolDistance, _patrolSpeed);
            IState nothingState = new NothingState();
            StateMachine.AddTransition(groundTwoWayPointsPatrol, nothingState, new PatrolToNothing(_triggerObserver));
            StateMachine.AddTransition(nothingState, groundTwoWayPointsPatrol, new NothingToPatrol(_triggerObserver));
            StateMachine.SetState(groundTwoWayPointsPatrol);
        }

        protected override void Update()
        {
            StateMachine.Update();
        }
    }
}