using Common;
using UnityEngine;

namespace EnemyScripts.AI.States
{
    public class GroundTwoWayPointsPatrol : IState
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly float[] _patrolPoints;
        private readonly Rigidbody2D _rigidbody;
        private readonly GameObject _view;

        private bool _reachedEndOfPath;
        private int _wayPointsCount;
        private Vector3 _lastPosition;

        public GroundTwoWayPointsPatrol(Transform transform, Rigidbody2D rigidbody, GameObject view,
            float patrolDistance, float speed)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _view = view;
            _speed = speed;

            Vector3 startPoint = _transform.position;
            _patrolPoints = new []{startPoint.x + patrolDistance, startPoint.x};
        }

        public void Enter()
        {
            _lastPosition = _transform.position;
        }

        public void Update()
        {
            if(Mathf.Abs(_patrolPoints[0] - _patrolPoints[1]) == 0) return;
            
            _reachedEndOfPath = Mathf.Abs(_transform.position.x - _patrolPoints[_wayPointsCount]) < .5;
            
            if (_reachedEndOfPath)
            {
                _wayPointsCount++;
            }
            
            if (_wayPointsCount >= _patrolPoints.Length)
                _wayPointsCount = 0;

            Vector2 position = _transform.position;
            Vector2 direction = new Vector2(_patrolPoints[_wayPointsCount] - position.x, position.y);
            //Vector2 force = direction * _speed * Time.deltaTime;
            //_rigidbody.AddForce(force);
            _rigidbody.MovePosition(position + direction * _speed * Time.deltaTime);
            FlipView();
            _lastPosition = position;
        }

        public void Exit()
        {
            _wayPointsCount = 0;
            
        }

        private void FlipView()
        {
            //Debug.Log($"position {_lastPosition.x - _transform.position.x}");
            if (_lastPosition.x - _transform.position.x >= 0.01f)
            {
                _view.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (_lastPosition.x - _transform.position.x <= -0.01f)
            {
                _view.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
