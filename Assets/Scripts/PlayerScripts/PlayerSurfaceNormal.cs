using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerSurfaceNormal
    {
        private readonly CapsuleCollider2D _collider;
        private readonly List<ContactPoint2D> _contactPoints;
        private const int GroundLayerMask = 1 << 7;
        private readonly ContactFilter2D _filter;
        private readonly Bounds _colliderBounds;

        public PlayerSurfaceNormal(Component player)
        {
            _filter = new ContactFilter2D();
            _filter.SetLayerMask(GroundLayerMask);
            _contactPoints = new List<ContactPoint2D>();
            if (!player.TryGetComponent(out CapsuleCollider2D collider)) return;
            _collider = collider;
            _colliderBounds = _collider.bounds;
        }

        public Vector3 Value()
        {
            if (!_collider || _collider.GetContacts(_filter, _contactPoints) <= 0) return Vector3.zero;
            var colliderCenterPoint = _colliderBounds.center.y;
            var colliderHalfHeight = _colliderBounds.size.y / 2;

            for (var i = 0; i < _contactPoints.Count; i++)
            {
                if(_contactPoints[i].point.y <= colliderCenterPoint - colliderHalfHeight)
                    return _contactPoints[i].normal;
            }
           
            return Vector3.zero;
        }
    }
}
