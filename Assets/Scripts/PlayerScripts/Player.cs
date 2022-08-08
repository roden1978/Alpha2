using System;
using System.Collections;
using Data;
using Services.PersistentProgress;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
    public class Player : MonoBehaviour, ISavedProgress, IHealth, IPositionAdapter, ILookDirection1Adapter
    {
        [field: SerializeField] public DoubleJumpSign DoubleJumpSign { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public PlayerView PlayerView { get; private set; }
        [SerializeField] private LostLifeFx _lostLifeFx;
        public Action<int> Death;
        public int LookDirection { get; set; }
        public int HP { get; private set; }
        public int MaxHealth { get; private set; }
        private int _currentLivesAmount;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if(HP < 0)
            {
                StartCoroutine(LostLifeFxShow());
                Death?.Invoke(_currentLivesAmount -= 1);
            }
        } 
        public void TakeHealth(int health)
        {
            if(HP < MaxHealth)
            {
                HP += health;
            }
        }

        public void TakeBonusLive(int delta)
        {
            _currentLivesAmount += delta;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            HP = playerProgress.PlayerState.CurrentHealth;
            _currentLivesAmount = playerProgress.PlayerState.CurrentLivesAmount;
            MaxHealth = playerProgress.PlayerState.MaxHealth;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.PlayerState.CurrentHealth = HP;
            playerProgress.PlayerState.CurrentLivesAmount = _currentLivesAmount;
        }

        private IEnumerator LostLifeFxShow()
        {
            _lostLifeFx.Show();
            yield return new WaitForSeconds(1f);
            _lostLifeFx.Hide();
        }

        public void SetLookDirection(int direction)
        {
            LookDirection = direction;
        }
    }
}
