using System;

namespace PlayerScripts
{
    [Serializable]
    public class PlayerData
    {
        public int LivesAmount = 3;
        public int Health = 50;
        public int FruitScoresAmount;
        public int CrystalsAmount;
        public int SceneIndex = 1;
        public int MaxHealth = 100;
        public int MaxBonusLivesCount = 6;
    }
}
