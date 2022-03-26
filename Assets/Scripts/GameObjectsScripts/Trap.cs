using System.Collections;
using EnemyScripts;
using PlayerScripts;
using StaticData;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Trap : PickableObject
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private TrapStaticData _trapStaticData;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;
        private void Start()
        {
            Value = _trapStaticData.Damage;
            Hide = false;
            _triggerObserver.TriggerEnter += OnDamageTriggerEnter;
            _triggerObserver.TriggerExit += OnDamageTriggerExit;
            _waitForSeconds = new WaitForSeconds(_trapStaticData.Cooldown);
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnDamageTriggerEnter;
            _triggerObserver.TriggerExit -= OnDamageTriggerExit;
        }

        private void OnDamageTriggerEnter(Collider2D other)
        {
            MakeDamage(other);
        }

        private void OnDamageTriggerExit(Collider2D other)
        {
            StopDamage();
        }

        private void MakeDamage(Component other)
        {
            _coroutine = StartCoroutine(Damage(other));
        }

        private void StopDamage()
        {
            StopCoroutine(_coroutine);
        }

        private IEnumerator Damage(Component other)
        {
            InteractableObjectsCollector player = other.GetComponent<InteractableObjectsCollector>();
            while(player.gameObject.activeInHierarchy)
            {
                player.Collect(this);
                yield return _waitForSeconds;
            }
        }
    }
}