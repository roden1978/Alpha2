using PlayerScripts;

namespace Infrastructure
{
    public class GamePlayerData
    {
        private PlayerData _playerData;
        
        public int CurrentLivesAmount;
        public int CurrentHealth;
        public int CurrentFruitScoresAmount;
        public int CurrentCrystalsAmount;
        public int CurrentScene;
        
        public GamePlayerData(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void InitializeGamePlayerData()
        {
            CurrentHealth = _playerData.Health;
            CurrentCrystalsAmount = _playerData.CrystalsAmount;
            CurrentLivesAmount = _playerData.LivesAmount;
            CurrentFruitScoresAmount = _playerData.FruitScoresAmount;
            CurrentScene = _playerData.SceneIndex;
        }
    }
}