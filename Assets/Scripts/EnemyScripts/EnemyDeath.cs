﻿using System;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        public Action Death;

        private void Start()
        {
            _health.HealthUpdate += OnHealthUpdate;
        }

        private void OnDestroy()
        {
            _health.HealthUpdate -= OnHealthUpdate;
        }

        private void OnHealthUpdate()
        {
            if(_health.HP <= 0)
                Die();
        }

        private void Die()
        {
            Death?.Invoke();
            _health.HealthUpdate -= OnHealthUpdate;
            Destroy(gameObject);
        }
    }
}