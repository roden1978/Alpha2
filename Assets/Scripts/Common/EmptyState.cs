using System;
using UnityEngine;

namespace Common
{
    public class EmptyState: BaseState
    {
        public EmptyState(GameObject gameObject) : base(gameObject)
        {
        }

        public override Type Tick()
        {
            throw new NotImplementedException();
        }

        public override Type FixedTick()
        {
            throw new NotImplementedException();
        }
    }
}