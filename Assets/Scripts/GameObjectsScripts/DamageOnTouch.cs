using System.Collections;
using EnemyScripts;
using PlayerScripts;
using StaticData;
using UnityEngine;

namespace GameObjectsScripts
{
    public class DamageOnTouch : PickableObject
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private EnemyStaticData _enemyStaticData;
        [SerializeField] private EnemyHealth _enemyHealth;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;
        private void Start()
        {
            Value = _enemyStaticData.DamageOnTouch;
            Hide = false;
            _triggerObserver.TriggerEnter += OnDamageTriggerEnter;
            _triggerObserver.TriggerExit += OnDamageTriggerExit;
            _waitForSeconds = new WaitForSeconds(_enemyStaticData.Cooldown);
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnDamageTriggerEnter;
            _triggerObserver.TriggerExit -= OnDamageTriggerExit;
        }

        private void OnDamageTriggerEnter(Collider2D other)
        {
            StartDamageOnTouch(other);
        }

        private void OnDamageTriggerExit(Collider2D other)
        {
            StopDamageOnTouch();
        }

        private void StartDamageOnTouch(Component other)
        {
            _coroutine = StartCoroutine(Damage(other));
        }

        private void StopDamageOnTouch()
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator Damage(Component other)
        {
            if(other.TryGetComponent(out InteractableObjectsCollector player))
            {
                while (player.gameObject.activeInHierarchy)
                {
                    player.Collect(this);
                    ShowCollectFX(player.transform);
                    SelfDamage();
                    yield return _waitForSeconds;
                }
            }
        }

        private void SelfDamage()
        {
            if (_enemyStaticData.SelfDamageOnTouch > 0)
            {
                _enemyHealth.TakeDamage(_enemyStaticData.SelfDamageOnTouch);
            }
        }
    }
}