using UnityEngine;

namespace PlayerScripts
{
    public class Dipstick : IDipstick
    {
        private const int GroundLayerMask = 1 << 7 | 1 << 16;
        
        private const int RayCount = 8;
        private readonly RaycastHit2D[] _results;
        private float _width;
        private readonly ILookDirection1Adapter _lookDirection1Adapter;
        private readonly IPositionAdapter _positionAdapter;
        private readonly Sprite _sprite;

        public Dipstick(ILookDirection1Adapter lookDirection1Adapter, IPositionAdapter positionAdapter, Sprite sprite)
        {
            _results = new RaycastHit2D[RayCount];
            _lookDirection1Adapter = lookDirection1Adapter;
            _positionAdapter = positionAdapter;
            _sprite = sprite;
        }

        private int DrawDipsticks()
        {
            _width = _sprite.bounds.size.x;
            float height = _sprite.bounds.size.y;
            Vector3 center = _sprite.bounds.center + new Vector3(_positionAdapter.Position.x, _positionAdapter.Position.y + height / 2, 0);
            Vector3 leftEdgePoint = new Vector3(center.x - _width / 2, center.y, 0);
            Vector3 rightEdgePoint = new Vector3(center.x + _width / 2, center.y, 0);
            int touchCount = 0;
            
            for (int i = 0; i <= RayCount - 3; i++)
            {
                /*Vector3 startPointV3 = _player.LookDirection > 0
                    ? new Vector3(leftEdgePoint.x + _width / RayCount * i, leftEdgePoint.y, 0)
                    : new Vector3(rightEdgePoint.x - _width / RayCount * i, rightEdgePoint.y, 0);*/
                Vector2 startPoint = _lookDirection1Adapter.LookDirection > 0 
                    ? new Vector2(leftEdgePoint.x + _width / RayCount * i, leftEdgePoint.y) 
                    : new Vector2(rightEdgePoint.x - _width / RayCount * i, rightEdgePoint.y);
                //Debug.DrawRay(startPointV3, Vector3.down * height / 2f, Color.red);
                touchCount += Physics2D.RaycastNonAlloc(startPoint, Vector2.down, _results, height / 2f, GroundLayerMask);
            }
            return touchCount;
        }

        public bool Contact()
        {
            return DrawDipsticks() > 0;
        }
        
        
    }
}
