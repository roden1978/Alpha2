using UnityEngine;

namespace EnemyScripts
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private EnemyHPBar _hpBar;
        [SerializeField] private EnemyHealth _health;

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
            _hpBar.SetValue(_health.HP, _health.MaxHealth);
        }
    }
}