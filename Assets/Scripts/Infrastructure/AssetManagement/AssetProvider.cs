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
    }
}