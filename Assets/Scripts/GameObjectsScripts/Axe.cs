using UnityEngine;

namespace GameObjectsScripts
{
    public class Axe : Weapon
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}
