using PlayerScripts;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class Mediator : MonoBehaviour
    {
        [SerializeField] private InteractableObjectsCollector _interactableObjectsCollector;
        [SerializeField] private Hud _hud;
        [SerializeField] private ControlPanel _controlPanel;
        [SerializeField] private SceneLoader _principal;
        [SerializeField] private Crowbar _crowbar;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _shootButton;
        private const int BonusScores = 1000;
        private const int BonusCrystals = 10;
        private GamePlayerData _gamePlayerData;

        private void Start()
        {
            _interactableObjectsCollector.FruitCollecting += OnFruitCollecting;
            _interactableObjectsCollector.CrystalCollecting += OnCrystalCollecting;
            _interactableObjectsCollector.FoodCollecting += OnFoodCollecting;
            _interactableObjectsCollector.LifeCollecting += OnLivesCollecting;
            //_principal.UpdateHud += UpdateHud;
            _gamePlayerData = Game.GamePlayerData;
            UpdateHud();
            _shootButton.onClick.AddListener(OnShoot);
            _pauseButton.onClick.AddListener(OnGamePause);
            if(Game.Mobile)
                _controlPanel.Show();
        }

        private void OnShoot()
        {
            _crowbar.Shoot();
        }

        private void OnDestroy()
        {
            _interactableObjectsCollector.FruitCollecting -= OnFruitCollecting;
            _interactableObjectsCollector.CrystalCollecting -= OnCrystalCollecting;
            _interactableObjectsCollector.FoodCollecting -= OnFoodCollecting;
            _interactableObjectsCollector.LifeCollecting -= OnLivesCollecting;
            //_principal.UpdateHud -= UpdateHud;
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
            Instantiate(_hud.LivesPanel.BonusLifeUI, _hud.LivesPanel.transform);
        }


        private void InitializeBonusLifeAmount(int currentLivesAmount)
        {
            if (_hud.LivesPanel.transform.childCount != Game.GamePlayerData.CurrentLivesAmount)
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
            if(_gamePlayerData.CurrentHealth < _gamePlayerData.MaxHealth)
            {
                _gamePlayerData.CurrentHealth += amount;
                UpdateHealthBar(_gamePlayerData.CurrentHealth);
            }
        }

        private void UpdateHealthBar(int currentHealth)
        {
            _hud.HealthBar.value = (float) currentHealth / _gamePlayerData.MaxHealth;
        }

        private void OnCrystalCollecting(int amount)
        {
            _gamePlayerData.CurrentCrystalsAmount += amount;
            UpdateCrystalsAmount(_gamePlayerData.CurrentCrystalsAmount);   
        }

        private void UpdateCrystalsAmount(int amount)
        {
            _hud.CrystalsAmount.text = amount.ToString();
        }

        private void OnFruitCollecting(int amount)
        {
            _gamePlayerData.CurrentFruitScoresAmount += amount;
            UpdateFruitAmount(_gamePlayerData.CurrentFruitScoresAmount);
        }

        private void UpdateFruitAmount(int amount)
        {
            _hud.FruitsAmount.text = amount.ToString();
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