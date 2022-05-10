using Common;
using UnityEngine;

namespace EnemyScripts.AI.States
{
    public class SpiderAttackState : IState
    {
        private readonly Animator _animator;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public SpiderAttackState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
            _animator.SetBool(Attack, true);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _animator.SetBool(Attack, false);
        }
    }
}