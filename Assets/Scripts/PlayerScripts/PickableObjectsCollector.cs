using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class PickableObjectsCollector : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PickableObject obj))
            {
                Debug.Log(obj.Price);
            }
        }

    }
}
