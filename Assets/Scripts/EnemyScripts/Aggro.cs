﻿using System.Collections;
using GameObjectsScripts;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private float _cooldown;
        private Coroutine _shooting;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnAggroTriggerEnter;
            _triggerObserver.TriggerExit += OnAggroTriggerExit;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnAggroTriggerEnter;
            _triggerObserver.TriggerExit -= OnAggroTriggerExit;
        }

        public void Construct(float cooldown)
        {
            _cooldown = cooldown;
        }

        private void OnAggroTriggerEnter(Collider2D obj) => 
            ShootOn(obj);

        private void OnAggroTriggerExit(Collider2D obj) => 
            ShootOff();

        private void ShootOn(Component target)
        {
            _shooting = StartCoroutine(Shooting(target.transform));
        }

        private void ShootOff()
        {
            StopCoroutine(_shooting);
        }

        private IEnumerator Shooting(Transform target)
        {
            //var lookDirection = LookDirection();
            //if(gameObject.transform.position.x > target.position.x) return

            while(gameObject.activeInHierarchy)
            {
                Vector3 position = _shootPoint.transform.position;
                Vector2 lookDirection = LookDirection();
                Vector2 targetSide = TargetSide(target);
                if(targetSide != lookDirection)
                    Flip();
                Bullet bullet = Instantiate(_bullet, position, Quaternion.identity);
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = DirectionToTarget(target.position  + Vector3.up, position);
                bulletRigidbody.AddForce(direction * bullet.Speed, ForceMode2D.Impulse);
                yield return new WaitForSeconds(_cooldown);
            }
        }
        private Vector2 DirectionToTarget(Vector3 target, Vector3 position)
        {
            Vector3 direction = target - position;
            Debug.Log($"Direction to player {direction.normalized}");
            return direction.normalized;
        }
        private void Flip()
        {
            transform.Rotate(0f, 180f, 0f);
        }
        
        private Vector2 LookDirection()
        {
            float shootPointPosition = _shootPoint.transform.position.x;
            float viewTransformPosition = transform.position.x;
            return shootPointPosition > viewTransformPosition ? Vector2.right : Vector2.left;
        }

        private Vector2 TargetSide(Transform target)
        {
            return target.position.x > transform.position.x ? Vector2.right : Vector2.left;
        }
    }

    /*public class LookAt
    {
        private readonly Component _view;
        private readonly ShootPoint _shootPoint;
        private double _prevDirection;

        public LookAt(Component view)
        {
            _view = view;
            _shootPoint = view.GetComponentInChildren<ShootPoint>();
        }
       
        public void FLippingView(float direction)
        {
            if(direction == 0) return;
            if(Math.Abs(CurrentLookDirection().x - direction) != 0)
                VerticalFlip();
            _prevDirection = direction;
        }

        private Vector2 CurrentLookDirection()
        {
            float shootPointPosition = _shootPoint.transform.position.x;
            float viewTransformPosition = _view.transform.position.x; 
            return shootPointPosition > viewTransformPosition ? Vector2.right : Vector2.left;
        }
        
    }*/
}