using System;
using Data;
using PlayerScripts;
using Services.PersistentProgress;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class Mediator : MonoBehaviour, ISavedProgress
    {
        private const int BonusScores = 1000;
        private const int BonusCrystals = 10;

        private InteractableObjectsCollector _interactableObjectsCollector;
        private Hud _hud;
        private ControlsPanel _controlsPanel;
        private Player _player;
        
        private int _currentFruitScoreAmount;
        private int _currentCrystalsAmount;
        private int _currentLivesAmount;
        private int _currentHealth;
        private int _maxHealth;
        private int _maxBonusLivesCount;

        public void Construct(InteractableObjectsCollector interactableObjectsCollector,
            Hud hud, ControlsPanel controlsPanel, Player player)
        {
            _interactableObjectsCollector = interactableObjectsCollector;
            _hud = hud;
            _controlsPanel = controlsPanel;
            _player = player;
            
            _interactableObjectsCollector.FruitCollecting += OnFruitCollecting;
            _interactableObjectsCollector.CrystalCollecting += OnCrystalCollecting;
            _interactableObjectsCollector.FoodCollecting += OnFoodCollecting;
            _interactableObjectsCollector.LifeCollecting += OnLivesCollecting;
            _interactableObjectsCollector.DamageCollecting += OnDamageCollecting;
            _player.Death += OnPlayerDeath;
        }

        private void OnPlayerDeath(int delta)
        {
            _currentLivesAmount = delta;
            if (_currentLivesAmount < 0)
            {
                Time.timeScale = 0f;
                _controlsPanel.GameMenu.Died.gameObject.SetActive(true);
                //Open GameOver panel
            }
            Debug.Log("Death");
            _player.TakeHealth(_maxHealth);
            _currentHealth = _maxHealth;
            UpdateHud();
            //Reload Level
        }

        private void OnDamageCollecting(int amount)
        {
            _currentHealth -= amount;
            _player.TakeDamage(amount);
            UpdateHealthBar(_currentHealth);
        }

        private void OnDestroy()
        {
            _interactableObjectsCollector.FruitCollecting -= OnFruitCollecting;
            _interactableObjectsCollector.CrystalCollecting -= OnCrystalCollecting;
            _interactableObjectsCollector.FoodCollecting -= OnFoodCollecting;
            _interactableObjectsCollector.LifeCollecting -= OnLivesCollecting;
            _interactableObjectsCollector.DamageCollecting -= OnDamageCollecting;
        }

        private void OnLivesCollecting(int amount)
        {
            if(_currentLivesAmount < _maxBonusLivesCount)
            {
                _currentLivesAmount += amount;
                _player.TakeBonusLive(amount);
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
            Instantiate(_hud.LivesPanel.BonusLifeUI, _hud.LivesPanel.transform);
        }


        private void InitializeBonusLifeAmount(int currentLivesAmount)
        {
            if (_hud != null && _hud.LivesPanel.transform.childCount != _currentLivesAmount)
            {
                var items = _hud.LivesPanel.GetComponentsInChildren(typeof(BonusLifeUI));
                foreach (Component item in items)
                {
                    Destroy(item.gameObject);
                }

                for (int i = 0; i < currentLivesAmount; i++)
                {
                    Instantiate(_hud.LivesPanel.BonusLifeUI, _hud.LivesPanel.transform);
                }
            }
        }

        private void OnFoodCollecting(int amount)
        {
            if(_currentHealth < _maxHealth)
            {
                _currentHealth += amount;
                _player.TakeHealth(amount);
                UpdateHealthBar(_currentHealth);
            }
        }

        private void UpdateHealthBar(int currentHealth)
        {
            float hp = Convert.ToSingle(currentHealth) / Convert.ToSingle(_maxHealth);
            Debug.Log(hp);
            _hud.HealthBar.fillAmount = hp;
            SetColour(hp);
        }
        private void SetColour(float current)
        {
            float twoThirds = Convert.ToSingle(_maxHealth) / 3 * 2 / _maxHealth; 
            float oneThird = Convert.ToSingle(_maxHealth) / 3 / _maxHealth;
            _hud.HealthBar.color = current <  twoThirds  && current > oneThird ? Color.yellow :
                current < oneThird ? Color.red : Color.green;
        }

        private void OnCrystalCollecting(int amount)
        {
            _currentCrystalsAmount += amount;
            UpdateCrystalsAmount(_currentCrystalsAmount);   
        }

        private void UpdateCrystalsAmount(int amount)
        {
            _hud.CrystalsAmount.text = amount.ToString();
        }

        private void OnFruitCollecting(int amount)
        {
            _currentFruitScoreAmount += amount;
            UpdateFruitAmount(_currentFruitScoreAmount);
        }

        private void UpdateFruitAmount(int amount)
        {
            _hud.FruitsAmount.text = amount.ToString();
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