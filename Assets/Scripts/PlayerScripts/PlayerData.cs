using System;

namespace PlayerScripts
{
    [Serializable]
    public class PlayerData
    {
        public int LivesAmount;
        public int Health;
        public int FruitScoresAmount;
        public int CrystalsAmount;
        public int SceneIndex = 1;
    }
}
