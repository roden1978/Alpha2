using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpProxyState : BaseState
    {
        private readonly Animator _animator;
        
        public JumpProxyState(GameObject player) : base(player)
        {
            _animator = player.GetComponentInChildren<Animator>();
        }

        public override Type Tick()
        {
            if (_animator.GetBool(JumpThrow) &&
                !_animator.GetBool(Jump)) 
                return typeof(JumpThrowState);
            
            return _animator.GetBool(Jump) == false ? 
                typeof(IdleState) : 
                typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return typeof(EmptyState);
        }
    }
}