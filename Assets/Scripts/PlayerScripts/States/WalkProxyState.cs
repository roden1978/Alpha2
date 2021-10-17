using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkProxyState : BaseState
    {
        private readonly Animator _animator;
        
        public WalkProxyState(GameObject player) : base(player)
        {
            _animator = player.GetComponentInChildren<Animator>();
        }

        public override Type Tick()
        {
            if (_animator.GetBool(WalkThrow) &&
                !_animator.GetBool(Walk)) return typeof(WalkThrowState);
            
            return _animator.GetBool(Walk) == false ? 
                typeof(IdleState) : 
                typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return typeof(EmptyState);
        }
    }
}