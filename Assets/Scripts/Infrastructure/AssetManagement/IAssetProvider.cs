using Infrastructure.Services;
using PlayerScripts;
using UI;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        ControlsPanel InstantiateControlsPanel();
        Crowbar InstantiateCrowbar();
        Player InstantiatePlayer();
        Crosshair InstantiateCrosshair();
    }
}