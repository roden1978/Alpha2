using System;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        void Load(int index, Action callback = null);
        void UnLoad(int index, Action callback = null);
    }
}