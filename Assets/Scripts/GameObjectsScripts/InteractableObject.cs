using UnityEngine;

namespace GameObjectsScripts
{
    public abstract class InteractableObject: MonoBehaviour
    {
        [SerializeField] protected int _value;
        [SerializeField] protected float _speed;
        public int Value => _value;
    }
}