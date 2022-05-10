using Common;
using EnemyScripts.AI.Conditions;
using EnemyScripts.AI.States;
using PlayerScripts;
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
            IState attackState = new SpiderAttackState(_model.GetComponent<Animator>());
            StateMachine.AddTransition(groundTwoWayPointsPatrol, attackState, new PatrolToAttack(_triggerObserver));
            StateMachine.AddTransition(attackState, groundTwoWayPointsPatrol, new AttackToPatrol(_triggerObserver));
            StateMachine.SetState(groundTwoWayPointsPatrol);
        }

        protected override void Update()
        {
            StateMachine.Update();
        }
    }
}