using System;

namespace Data
{
    [Serializable]
    public class PlayerState
    {
        public int CurrentLivesAmount;
        public int CurrentHealth;
        public int CurrentFruitScoresAmount;
        public int CurrentCrystalsAmount;
        public int MaxHealth;
        public int MaxBonusLivesCount;
        public int StartLivesAmount;

        public void ResetHP() => CurrentHealth = MaxHealth;

        public void ResetLiveAmount() => CurrentLivesAmount = StartLivesAmount;

    }
}