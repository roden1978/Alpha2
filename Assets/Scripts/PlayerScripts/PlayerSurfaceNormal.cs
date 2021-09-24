using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerSurfaceNormal
    {
        private readonly Player _player;
        private readonly CapsuleCollider2D _collider;
        private readonly List<ContactPoint2D> _list;
        private const int GroundLayerMask = 1 << 7;
        private readonly ContactFilter2D _filter;

        public PlayerSurfaceNormal(Player player)
        {
            _player = player;
            _filter = new ContactFilter2D();
            _filter.SetLayerMask(GroundLayerMask);
            _list = new List<ContactPoint2D>();
            if (_player.TryGetComponent(out CapsuleCollider2D collider))
            {
                _collider = collider;
            }
        }

        public Vector3 Value()
        {
            if (!_collider || _collider.GetContacts(_filter, _list) <= 0) return Vector3.zero;
            foreach (var contactPoint in _list
                .Where(contactPoint => contactPoint.point.y < _player.transform.position.y))
            {
                return contactPoint.normal;
            }
            return Vector3.zero;
        }
    }
}
