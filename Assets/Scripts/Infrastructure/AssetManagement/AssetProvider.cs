using UI;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public ControlsPanel Instantiate()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.ControlsPanelPath);
            return Object.Instantiate(prefab).GetComponent<ControlsPanel>();
        }
    }
}