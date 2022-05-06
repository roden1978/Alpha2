using Common;
using Pathfinding;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts.AI.States
{
    public class ChaseState : IState
    {
        private const int NextWayPointDistance = 1;
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly GameObject _view;
        private readonly float _speed;
        private readonly PathBuilder _pathBuilder;
        private readonly Player _player;
        private Path _path;
        private int _currentWayPoint;
        private bool _reachedEndOfPath;

        public ChaseState(Transform transform,PathBuilder pathBuilder, float speed, Player player,
            Rigidbody2D rigidbody, GameObject view)
        {
            _transform = transform;
            _pathBuilder = pathBuilder;
            _speed = speed;
            _player = player;
            _rigidbody = rigidbody;
            _view = view;
        }
        
        public void Enter()
        {
            Vector3 playerPosition = _player.transform.position;
            _path = _pathBuilder.Build(_transform.position, playerPosition);
        }

        public void Update()
        {
            if(_path == null) return;
            _reachedEndOfPath = _currentWayPoint >= _path.vectorPath.Count; 
            if (_reachedEndOfPath)
            {
                _path = _pathBuilder.Build(_transform.position, _player.transform.position);
                _currentWayPoint = 0;
            }
            if (_reachedEndOfPath) return;

            if(_path != null)
            {
                Vector3 position = _transform.position;
                Vector2 direction = (_path.vectorPath[_currentWayPoint] - position).normalized;
                Vector2 force = direction * (_speed * Time.deltaTime);
                _rigidbody.AddForce(force);
                float distance = Vector2.Distance(position, _path.vectorPath[_currentWayPoint]);

               if (distance < NextWayPointDistance)
                    _currentWayPoint++;
            }

            FlipView();
        }

        public void Exit()
        {
            Debug.Log("Chase state exit");
            _path = null;
            _currentWayPoint = 0;
        }

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
