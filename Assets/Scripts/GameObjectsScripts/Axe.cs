using System.Collections;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Axe : Weapon
    {
        private void OnEnable()
        {
            StartCoroutine(ReturnToPool());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            gameObject.SetActive(false);
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(LifeTime);
            gameObject.SetActive(false);
        }
                
    }
}
