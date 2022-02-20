using Infrastructure.Services;
using UI;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        ControlsPanel Instantiate();
    }
}