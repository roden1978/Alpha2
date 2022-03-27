using System;
using Infrastructure.GameStates;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        void Load(string name, Action callback = null);
        void UnLoad(int index, Action callback = null);
    }
}