using Common;
using Pathfinding;
using UnityEngine;

namespace EnemyScripts.AI.States
{
    public class TwoWayPointsPatrol : IState
    {
        private const int NextWayPointDistance = 1;
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly PathBuilder _pathBuilder;
        private readonly Path[] _paths;
        private readonly Rigidbody2D _rigidbody;
        private readonly GameObject _view;
        private readonly float _patrolDistance;
        private readonly Vector3 _startPoint;
        private bool _reachedEndOfPath;
        private int _currentWayPoint;
        private int _wayPointsCount;

        public TwoWayPointsPatrol(Transform transform, Rigidbody2D rigidbody, GameObject view,
            float patrolDistance, float speed, PathBuilder pathBuilder)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _view = view;
            _patrolDistance = patrolDistance;
            _speed = speed;
            _pathBuilder = pathBuilder;
            
            _paths = new Path[2];
            _startPoint = _transform.position;
        }

        public void Enter()
        {
            _paths[0] = _pathBuilder.Build(_startPoint, new Vector3(_startPoint.x + _patrolDistance, _startPoint.y, 0));
            _paths[1] ??= _pathBuilder.Build(new Vector3(_startPoint.x + _patrolDistance, _startPoint.y, 0), _startPoint);
        }

        public void Update()
        {
            if(_paths[0] == null || _paths[1] == null) return;
            
            _reachedEndOfPath = _currentWayPoint >= _paths[_wayPointsCount].vectorPath.Count;
            
            if (_reachedEndOfPath)
            {
                _wayPointsCount++;
                _currentWayPoint = 0;
            }
            
            if (_wayPointsCount >= _paths.Length)
                _wayPointsCount = 0;
            
            if (_reachedEndOfPath) return;

            Vector3 position = _transform.position;
            Vector2 direction = (_paths[_wayPointsCount].vectorPath[_currentWayPoint] - position).normalized;
            Vector2 force = direction * (_speed * Time.deltaTime);
            _rigidbody.AddForce(force);
            float distance = Vector2.Distance(position, 
                _paths[_wayPointsCount].vectorPath[_currentWayPoint]);

            if (distance < NextWayPointDistance)
                _currentWayPoint++;
            
            FlipView();
        }

        public void Exit() => 
            _wayPointsCount = _currentWayPoint = 0;

        private void FlipView()
        {
            if (_rigidbody.velocity.x >= 0.01f)
            {
                _view.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (_rigidbody.velocity.x <= -0.01f)
            {
                _view.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
}
