using System;

namespace Data
{
    [Serializable]
    public class StaticPlayerData
    {
        public int StartLivesAmount = 3;
        public int Health = 50;
        public int FruitScoresAmount;
        public int CrystalsAmount;
        public int MaxHealth = 100;
        public int MaxBonusLivesCount = 6;
    }
}
