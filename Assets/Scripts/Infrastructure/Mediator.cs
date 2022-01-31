using PlayerScripts;
using UnityEngine;

namespace Infrastructure
{
    public class Mediator : MonoBehaviour
    {
        [SerializeField] private InteractableObjectsCollector _interactableObjectsCollector;
        [SerializeField] private Hud _hud;

        private GamePlayerData _gamePlayerData;

        private void Start()
        {
            _interactableObjectsCollector.FruitCollecting += OnFruitCollecting;
            _gamePlayerData = Game.GamePlayerData;
            StartHud(Game.GamePlayerData);
        }

        private void OnDestroy()
        {
            _interactableObjectsCollector.FruitCollecting -= OnFruitCollecting;
        }

        private void OnFruitCollecting(int scores)
        {
            _gamePlayerData.CurrentFruitScoresAmount += scores;
            UpdateFruitAmount(_gamePlayerData.CurrentFruitScoresAmount);
        }

        private void StartHud(GamePlayerData gamePlayerData)
        {
            UpdateFruitAmount(gamePlayerData.CurrentFruitScoresAmount);
        }

        private void UpdateFruitAmount(int amount)
        {
            _hud.FruitsAmount.text = amount.ToString();
        }
    }
}