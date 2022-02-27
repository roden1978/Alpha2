using System.Collections.Generic;
using Infrastructure.Services;
using PlayerScripts;
using UI;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        public ControlsPanel CreateControlsPanel();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        Crowbar CreateCrowbar();
    }
}