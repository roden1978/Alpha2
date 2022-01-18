﻿using GameObjectsScripts;
using UnityEngine;

namespace Services.Pools
{
    public abstract class PooledObject : MonoBehaviour, ISwitchable
    {
        public abstract void ReturnToPool();
        public void Show() => gameObject.SetActive(true); 
        public void Hide() => gameObject.SetActive(false);
        
    }
}