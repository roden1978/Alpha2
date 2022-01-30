using UnityEngine;

namespace PlayerScripts.States
{
    public sealed class PlayerStateData
    {
        public bool IsShoot { get; set; }
        public bool IsOnGround { get; set; }
        public Vector2 Damping { get; set; }
    }
}