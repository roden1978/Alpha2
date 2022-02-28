using Infrastructure.Services;
using PlayerScripts;
using Services.Pools;
using UI;

namespace Infrastructure.GameStates
{
    public struct StatesPayload
    {
        public InteractableObjectsCollector InteractableObjectsCollector;
        public Player Player;
        public Hud Hud;
        public Crowbar Crowbar;
        public ControlsPanel ControlsPanel;
        public PoolService Pool;
        public string CurrentSceneName;
        public int CurrentSceneIndex;
    }
}