using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int _health;

        public int HP => _health;

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if(_health < 0)
                Destroy(gameObject);
        }

        public void TakeHealth(int health)
        {
            //
        }
    }
}