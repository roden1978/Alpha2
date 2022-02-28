using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class AttackShooting : MonoBehaviour
    {
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] [Range(.1f, 1f)] private float _cooldown;
        private void Start()
        {
            Debug.Log("Attack");
        }
    }
}