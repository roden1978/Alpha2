using PlayerScripts;
using UI;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public ControlsPanel InstantiateControlsPanel()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.ControlsPanelPath);
            return Object.Instantiate(prefab).GetComponent<ControlsPanel>();
        }

        public Crowbar InstantiateCrowbar()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.CrowbarPath);
            return Object.Instantiate(prefab).GetComponent<Crowbar>();
        }

        public Player InstantiatePlayer()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.PlayerPath);
            return Object.Instantiate(prefab).GetComponent<Player>();
        }

        public Crosshair InstantiateCrosshair()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.CrosshairPath);
            return Object.Instantiate(prefab).GetComponent<Crosshair>();
        }

        public Mediator InstantiateMediator()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.MediatorPath);
            return Object.Instantiate(prefab).GetComponent<Mediator>();
        }
    }
}