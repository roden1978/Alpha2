using UnityEngine;

namespace PlayerScripts
{
    public class Radar
    {
        private const int FullDegree = 360;
        private const int EnemyLayerMask = 1 << 13;

        private readonly float _distance;
        private readonly float _initDeltaDegree;
        private readonly int _endDegree;
        private readonly int _degree;

        private float _delta;
        private float _currentDegree;

        private Vector3 _direction;
        private Vector2 _currentHit;

        private bool _clockwise;

        public Radar(float distance, float delta, int degree)
        {
            _distance = distance;
            _degree = degree;
            _endDegree = FullDegree - _degree;
            _currentDegree = degree;
            _delta = delta;
            _initDeltaDegree = delta;
            _clockwise = true;
        }

        public Vector2 Target(float lookDirection, Vector3 position)
        {
            _currentHit = SearchTarget(lookDirection, position);
            _delta = _currentHit != Vector2.zero ? _initDeltaDegree / 100 : _initDeltaDegree;

            if (_currentHit != Vector2.zero)
                return _currentHit;

            _currentHit = Vector2.zero;
            return Vector2.zero;
        }

        private Vector2 SearchTarget(float lookDirection, Vector3 position)
        {
            _direction.x = Mathf.Cos(DegreeToRadians(_currentDegree)) * lookDirection;
            _direction.y = Mathf.Sin(DegreeToRadians(_currentDegree));
            RaycastHit2D hit = Physics2D.Raycast(position, _direction, _distance, EnemyLayerMask);
            //remove before production
            Debug.DrawRay(position, _direction * _distance, Color.red);
            //remove before production
            _currentDegree = _clockwise ? NextDegreeClockwise() : NextDegreeCounterclockwise();

            return hit.collider != null ? hit.point : Vector2.zero;
        }

        private static float DegreeToRadians(float degree)
        {
            return degree * Mathf.Deg2Rad;
        }

        private float NextDegreeClockwise()
        {
            _currentDegree -= _delta;
            if (_currentDegree < 0) _currentDegree = FullDegree - _currentDegree;
            if (_currentDegree < _endDegree && _currentDegree > _degree) _clockwise = false;
            return _currentDegree;
        }

        private float NextDegreeCounterclockwise()
        {
            _currentDegree += _delta;
            if (_currentDegree > FullDegree) _currentDegree -= FullDegree;
            if (_currentDegree > _degree && _currentDegree < _endDegree) _clockwise = true;
            return _currentDegree;
        }
    }
}