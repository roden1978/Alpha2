using UnityEngine;

namespace PlayerScripts
{
    public class Dipstick
    {
        private const int RayCount = 4;
        private readonly RaycastHit2D[] _results;
        private readonly float _width;
        private readonly CapsuleCollider2D _capsule;

        private const int GroundLayerMask = 1 << 7;
        public Dipstick(Component player)
        {
            _results = new RaycastHit2D[4];

            if (player.TryGetComponent(out CapsuleCollider2D capsule))
            {
                _capsule = capsule;
                _width = _capsule.bounds.size.x;
            }
        }

        private int DrawDipsticks()
        {
            var bounds = _capsule.bounds;
            var height = bounds.size.y;
            var center = bounds.center;
            var leftEdgePoint = new Vector3(center.x - _width / 2, center.y, 0);
            var touchCount = 0;
            
            for (var i = 0; i < RayCount; i++)
            {
                var startPointV3 = new Vector3(leftEdgePoint.x + _width / RayCount * i, leftEdgePoint.y, 0);
                var startPoint = new Vector2(leftEdgePoint.x + _width / RayCount * i, leftEdgePoint.y);
                Debug.DrawRay(startPointV3, Vector3.down * height / 1.8f, Color.red);
                touchCount += Physics2D.RaycastNonAlloc(startPoint, Vector2.down, _results, height / 1.8f, GroundLayerMask);
            }
            return touchCount;
        }

        public bool Contact()
        {
            return DrawDipsticks() > 0;
        }
    }
}
