using UI;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        ControlsPanel Instantiate();
    }
}