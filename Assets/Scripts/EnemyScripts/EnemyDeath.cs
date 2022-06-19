using System;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private ParticleSystem _deathFx;
        public Action Death;

        private void OnEnable()
        {
            _health.HealthUpdate += OnHealthUpdate;
        }

        private void OnDisable()
        {
            _health.HealthUpdate -= OnHealthUpdate;
        }

        private void OnHealthUpdate()
        {
            if(_health.HP <= 0)
            {
                PlayDeathFx();
                Die();
            }
        }

        private void Die()
        {
            Death?.Invoke();
            _health.HealthUpdate -= OnHealthUpdate;
            Destroy(gameObject);
        }

        private void PlayDeathFx()
        {
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}