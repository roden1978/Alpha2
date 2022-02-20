using Infrastructure.AssetManagement;
using UI;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public ControlsPanel CreateControlsPanel() => 
            _assetProvider.Instantiate();
    }
}