using System;
using UnityEngine;
namespace PlayerScripts
{
    public class FlipView
    {
        private readonly PlayerView _playerView;
        private float _prevDirection;

        public FlipView(PlayerView playerView)
        {
            _playerView = playerView;
            _prevDirection = Vector2.right.x;
        }

        private void VerticalFlip()
        {
            _playerView.transform.Rotate(0f, 180f, 0f);
        }
        
        public void FLippingPlayerView(float direction)
        {
            if(direction == 0) return;
            if(Math.Abs(_prevDirection - direction) != 0)
                VerticalFlip();
            _prevDirection = direction;
        }
    }
}
