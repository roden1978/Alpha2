using Data;
using PlayerScripts;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class Mediator : MonoBehaviour, ISavedProgress
    {
        private const int BonusScores = 1000;
        private const int BonusCrystals = 10;
        
        public InteractableObjectsCollector InteractableObjectsCollector;
        public Hud Hud;
        public ControlsPanel ControlsPanel;
        public Player Player;
        
        private int _currentFruitScoreAmount;
        private int _currentCrystalsAmount;
        private int _currentLivesAmount;
        private int _currentHealth;
        private int _maxHealth;
        private int _maxBonusLivesCount;

        private void Start()
        {
            InteractableObjectsCollector.FruitCollecting += OnFruitCollecting;
            InteractableObjectsCollector.CrystalCollecting += OnCrystalCollecting;
            InteractableObjectsCollector.FoodCollecting += OnFoodCollecting;
            InteractableObjectsCollector.LifeCollecting += OnLivesCollecting;
            InteractableObjectsCollector.DamageCollecting += OnDamageCollecting;
            Player.Death += OnPlayerDeath;
            //UpdateHud();
            if(ControlsPanel != null)
                ControlsPanel.Show();
        }

        private void OnPlayerDeath(int delta)
        {
            _currentLivesAmount -= delta;
            if (_currentLivesAmount < 0)
            {
                Debug.Log("GameOver");
                //Open GameOver panel
            }
            Debug.Log("Death");
            //Reload Level
        }

        private void OnDamageCollecting(int amount)
        {
            _currentHealth -= amount;
            Player.TakeDamage(amount);
            UpdateHealthBar(_currentHealth);
        }

        private void OnDestroy()
        {
            InteractableObjectsCollector.FruitCollecting -= OnFruitCollecting;
            InteractableObjectsCollector.CrystalCollecting -= OnCrystalCollecting;
            InteractableObjectsCollector.FoodCollecting -= OnFoodCollecting;
            InteractableObjectsCollector.LifeCollecting -= OnLivesCollecting;
            InteractableObjectsCollector.DamageCollecting += OnDamageCollecting;
        }

        private void OnLivesCollecting(int amount)
        {
            if(_currentLivesAmount < _maxBonusLivesCount)
            {
                _currentLivesAmount += amount;
                Player.TakeBonusLive(amount);
                UpdateBonusLivesCount();
            }
            else
            {
                AccrueBonuses();
            }    
        }

        private void AccrueBonuses()
        {
            UpdateFruitAmount(_currentFruitScoreAmount + BonusScores);
            UpdateCrystalsAmount(_currentCrystalsAmount + BonusCrystals);
        }

        private void UpdateBonusLivesCount()
        {
            Instantiate(Hud.LivesPanel.BonusLifeUI, Hud.LivesPanel.transform);
        }


        private void InitializeBonusLifeAmount(int currentLivesAmount)
        {
            if (Hud.LivesPanel.transform.childCount != _currentLivesAmount)
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
            if(_currentHealth < _maxHealth)
            {
                _currentHealth += amount;
                Player.TakeHealth(amount);
                UpdateHealthBar(_currentHealth);
            }
        }

        private void UpdateHealthBar(int currentHealth)
        {
            Hud.HealthBar.value = (float) currentHealth / _maxHealth;
        }

        private void OnCrystalCollecting(int amount)
        {
            _currentCrystalsAmount += amount;
            UpdateCrystalsAmount(_currentCrystalsAmount);   
        }

        private void UpdateCrystalsAmount(int amount)
        {
            Hud.CrystalsAmount.text = amount.ToString();
        }

        private void OnFruitCollecting(int amount)
        {
            _currentFruitScoreAmount += amount;
            UpdateFruitAmount(_currentFruitScoreAmount);
        }

        private void UpdateFruitAmount(int amount)
        {
            Hud.FruitsAmount.text = amount.ToString();
        }

        private void UpdateHud()
        {
            UpdateFruitAmount(_currentFruitScoreAmount);
            UpdateCrystalsAmount(_currentCrystalsAmount);
            InitializeBonusLifeAmount(_currentLivesAmount);
            UpdateHealthBar(_currentHealth);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _currentFruitScoreAmount = playerProgress.PlayerState.CurrentFruitScoresAmount;
            _currentCrystalsAmount = playerProgress.PlayerState.CurrentCrystalsAmount;
            _currentLivesAmount = playerProgress.PlayerState.CurrentLivesAmount;
            _currentHealth = playerProgress.PlayerState.CurrentHealth;
            _maxHealth = playerProgress.PlayerState.MaxHealth;
            _maxBonusLivesCount = playerProgress.PlayerState.MaxBonusLivesCount;
            UpdateHud();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.PlayerState.CurrentFruitScoresAmount = _currentFruitScoreAmount;
            playerProgress.PlayerState.CurrentCrystalsAmount = _currentCrystalsAmount;
        }
    }
}