using System;
using UnityEngine;

namespace GameObjectsScripts
{
    public abstract class PickableObject: MonoBehaviour
    {
        [SerializeField] private int _price;
        public int Price => _price;
        
    }
}