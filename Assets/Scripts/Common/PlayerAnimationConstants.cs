using UnityEngine;

namespace Common
{
    public static class PlayerAnimationConstants
    {
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int IdleThrow = Animator.StringToHash("IdleThrow");
        public static readonly int WalkThrow = Animator.StringToHash("WalkThrow");
        public static readonly int JumpThrow = Animator.StringToHash("JumpThrow");
    }
}