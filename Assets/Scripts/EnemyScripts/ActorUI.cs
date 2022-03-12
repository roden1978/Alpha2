using UnityEngine;

namespace EnemyScripts
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private EnemyHPBar _hpBar;
        private EnemyHealth _health;

        private void OnDestroy()
        {
            _health.HealthUpdate -= OnHealthUpdate;
        }

        public void Construct(EnemyHealth health)
        {
            _health = health;
            _health.HealthUpdate += OnHealthUpdate;
        }
        private void OnHealthUpdate()
        {
            _hpBar.SetValue(_health.HP, _health.MaxHealth);
        }
    }
}