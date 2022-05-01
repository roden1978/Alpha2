using System.Collections;
using GameObjectsScripts;
using PlayerScripts;
using StaticData;
using UnityEngine;

namespace EnemyScripts
{
    public class OctonoidAttack : PickableObject
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private EnemyStaticData _enemyStaticData;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;
      
        private void Start()
        {
            Value = _enemyStaticData.DamageOnTouch;
            Hide = false;
            _triggerObserver.TriggerEnter += OnAttackTriggerEnter;
            _triggerObserver.TriggerExit += OnAttackTriggerExit;
            _waitForSeconds = new WaitForSeconds(_enemyStaticData.Cooldown);
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnAttackTriggerEnter;
            _triggerObserver.TriggerExit -= OnAttackTriggerExit;
        }

        private void OnAttackTriggerExit(Collider2D obj)
        {
            AttackOff();
        }

        private void OnAttackTriggerEnter(Collider2D obj)
        {
            AttackOn(obj);
        }
        
        private void AttackOn(Component other)
        {
            _coroutine = StartCoroutine(Damage(other));
        }

        private void AttackOff()
        {
            StopCoroutine(_coroutine);
        }

        private IEnumerator Damage(Component other)
        {
            Debug.Log(other.gameObject.name);
            if(other.TryGetComponent(out InteractableObjectsCollector player))
            {
                
                while (player.gameObject.activeInHierarchy)
                {
                    player.Collect(this);
                    yield return _waitForSeconds;
                }
            }
        }
    }
}
