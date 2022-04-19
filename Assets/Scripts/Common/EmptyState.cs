﻿using System;

namespace Common
{
    public class EmptyState: IState
    {
        public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}