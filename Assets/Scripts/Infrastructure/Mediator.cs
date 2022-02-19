using PlayerScripts;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class Mediator : MonoBehaviour
    {
        public InteractableObjectsCollector InteractableObjectsCollector;
        public Hud Hud;
        public Crowbar Crowbar;
        public ControlsPanel ControlsPanel;
        
        private const int BonusScores = 1000;
        private const int BonusCrystals = 10;
        private GamePlayerData _gamePlayerData;

        private void Start()
        {
            InteractableObjectsCollector.FruitCollecting += OnFruitCollecting;
            InteractableObjectsCollector.CrystalCollecting += OnCrystalCollecting;
            InteractableObjectsCollector.FoodCollecting += OnFoodCollecting;
            InteractableObjectsCollector.LifeCollecting += OnLivesCollecting;
            _gamePlayerData = Game.GamePlayerData;
            UpdateHud();
            if(ControlsPanel != null)
                ControlsPanel.Show();
        }

        private void OnShoot()
        {
            Crowbar.Shoot();
        }

        private void OnDestroy()
        {
            InteractableObjectsCollector.FruitCollecting -= OnFruitCollecting;
            InteractableObjectsCollector.CrystalCollecting -= OnCrystalCollecting;
            InteractableObjectsCollector.FoodCollecting -= OnFoodCollecting;
            InteractableObjectsCollector.LifeCollecting -= OnLivesCollecting;
        }

        private void OnLivesCollecting(int amount)
        {
            if(_gamePlayerData.CurrentLivesAmount < _gamePlayerData.MaxBonusLivesCount)
            {
                _gamePlayerData.CurrentLivesAmount += amount;
                UpdateBonusLivesCount();
            }
            else
            {
                AccrueBonuses();
            }    
        }

        private void AccrueBonuses()
        {
            UpdateFruitAmount(_gamePlayerData.CurrentFruitScoresAmount + BonusScores);
            UpdateCrystalsAmount(_gamePlayerData.CurrentCrystalsAmount + BonusCrystals);
        }

        private void UpdateBonusLivesCount()
        {
            Instantiate(Hud.LivesPanel.BonusLifeUI, Hud.LivesPanel.transform);
        }


        private void InitializeBonusLifeAmount(int currentLivesAmount)
        {
            if (Hud.LivesPanel.transform.childCount != Game.GamePlayerData.CurrentLivesAmount)
            {
                var items = Hud.LivesPanel.GetComponentsInChildren(typeof(BonusLifeUI));
                foreach (Component item in items)
                {
                    Destroy(item.gameObject);
                }

                for (int i = 0; i < currentLivesAmount; i++)
                {
                    Instantiate(Hud.LivesPanel.BonusLifeUI, Hud.LivesPanel.transform);
                }
            }
        }

        private void OnFoodCollecting(int amount)
        {
            if(_gamePlayerData.CurrentHealth < _gamePlayerData.MaxHealth)
            {
                _gamePlayerData.CurrentHealth += amount;
                UpdateHealthBar(_gamePlayerData.CurrentHealth);
            }
        }

        private void UpdateHealthBar(int currentHealth)
        {
            Hud.HealthBar.value = (float) currentHealth / _gamePlayerData.MaxHealth;
        }

        private void OnCrystalCollecting(int amount)
        {
            _gamePlayerData.CurrentCrystalsAmount += amount;
            UpdateCrystalsAmount(_gamePlayerData.CurrentCrystalsAmount);   
        }

        private void UpdateCrystalsAmount(int amount)
        {
            Hud.CrystalsAmount.text = amount.ToString();
        }

        private void OnFruitCollecting(int amount)
        {
            _gamePlayerData.CurrentFruitScoresAmount += amount;
            UpdateFruitAmount(_gamePlayerData.CurrentFruitScoresAmount);
        }

        private void UpdateFruitAmount(int amount)
        {
            Hud.FruitsAmount.text = amount.ToString();
        }

        private void UpdateHud()
        {
            UpdateFruitAmount(_gamePlayerData.CurrentFruitScoresAmount);
            UpdateCrystalsAmount(_gamePlayerData.CurrentCrystalsAmount);
            InitializeBonusLifeAmount(_gamePlayerData.CurrentLivesAmount);
            UpdateHealthBar(_gamePlayerData.CurrentHealth);
        }
        

        private void OnGamePause()
        {
            Game.Pause();
        }
    }
}