﻿using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class WalkThrowToWalk : ICondition
    {
        private readonly Animator _animator;

        public WalkThrowToWalk(Animator animator)
        {
            _animator = animator;
        }

        public bool Result()
        {
            Debug.Log("WalkThrowToWalk");
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && 
                   _animator.GetCurrentAnimatorStateInfo(0).IsName("WalkThrowWithAxe");
        }
    }
}