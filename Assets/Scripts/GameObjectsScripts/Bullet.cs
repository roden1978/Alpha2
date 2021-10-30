﻿using UnityEngine;

namespace GameObjectsScripts
{
    public abstract class Bullet : DamagingObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private int _lifeTime;

        public float Speed => _speed;
        protected int LifeTime => _lifeTime;

        protected override void Start()
        {
            base.Start();
            Value = _damage;
        }
    }
}