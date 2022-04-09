using UnityEngine;

namespace PlayerScripts
{
    public class Dipstick : IDipstick
    {
        private const int GroundLayerMask = 1 << 7;
        
        private readonly Player _player;
        private readonly Collider2D[] _results;

        public Dipstick(Player player)
        {
            _player = player;
            _results = new Collider2D[1];
        }

        private int DrawDipstick()
        {
            Vector2 position = new Vector2(_player.transform.position.x, _player.transform.position.y + .35f);
            return Physics2D.OverlapCircleNonAlloc(position, .4f, _results, GroundLayerMask);
        }

        public bool Contact()
        {
            return DrawDipstick() > 0;
        }
    }
}
