using UnityEngine;

namespace PlayerScripts
{
    public class StayOnGroundMarker : IStayOnGroundMarker
    {
        private readonly BoxCollider2D _boxCollider;

        private const int GroundLayerMask = 1 << 7;

        public StayOnGroundMarker(Component player)
        {
            if (player.TryGetComponent(out BoxCollider2D boxCollider))
            {
                _boxCollider = boxCollider;
            }
        }

        public bool Value()
        {
            return _boxCollider.IsTouchingLayers(GroundLayerMask);
        }
    }
}
