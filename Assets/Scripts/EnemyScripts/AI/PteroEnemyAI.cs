using Common;
using EnemyScripts.AI.Conditions;
using EnemyScripts.AI.States;
using Pathfinding;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts.AI
{
    public class PteroEnemyAI : EnemyAI
    {
        [SerializeField] private Seeker _seeker;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private GameObject _model;
        [Header("Patrol")]
        [SerializeField] private float _patrolSpeed;
        [SerializeField] private int _patrolDistance;
        [SerializeField] private int _distanceToPatrol;

        [Header("Attack")]
        [SerializeField] private float _attackSpeed;
        [SerializeField] private int _distanceToAttack;
        private PathBuilder _pathBuilder;

        public override void Construct(Player player)
        {
            Player = player;
            StateMachine = new StateMachine();
            _pathBuilder = new PathBuilder(_seeker);
            Transform enemyTransform = transform;
            IState twoWayPointsPatrol = new TwoWayPointsPatrol(enemyTransform, _rigidbody,_model, _patrolDistance, _patrolSpeed, _pathBuilder);
            IState chase = new ChaseState(enemyTransform, _pathBuilder,_attackSpeed, Player, _rigidbody,_model);
            StateMachine.AddTransition(twoWayPointsPatrol, chase, new ChaseDistance(Player, this, _distanceToAttack));
            StateMachine.AddTransition(chase, twoWayPointsPatrol, new PatrolDistance(Player, this, _distanceToPatrol));
            StateMachine.SetState(twoWayPointsPatrol);
        }

        protected override void Update()
        {
            StateMachine.Update();
        }
    }
}