using GameObjectsScripts;
using Infrastructure.Services;
using PlayerScripts;
using UI;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        ControlsPanel InstantiateControlsPanel();
        Crowbar InstantiateCrowbar();
        Player InstantiatePlayer();
        Crosshair InstantiateCrosshair();
        Mediator InstantiateMediator();
        PickableObject InstantiateLoot();
    }
}