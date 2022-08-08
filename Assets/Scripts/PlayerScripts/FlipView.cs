using System;
using UnityEngine;
namespace PlayerScripts
{
    public class FlipView: IFlipView
    {
        private readonly Component _playerView;
        private float _prevDirection;

        public FlipView(Component playerView)
        {
            _playerView = playerView;
            _prevDirection = Vector2.right.x;
        }

        private void VerticalFlip()
        {
            _playerView.transform.Rotate(0f, 180f, 0f);
        }
        
        public float FLippingPlayerView(float direction)
        {
            if(direction != 0)
            {
                if (Math.Abs(_prevDirection - direction) != 0)
                    VerticalFlip();
                _prevDirection = direction;
            }
            return _prevDirection;
        }
    }
}
