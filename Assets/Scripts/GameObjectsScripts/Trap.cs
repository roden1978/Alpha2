using System.Collections;
using EnemyScripts;
using PlayerScripts;
using StaticData;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private TrapStaticData _trapStaticData;
        private Coroutine _coroutine;
        private void Start()
        {
            _triggerObserver.TriggerEnter += OnDamageTriggerEnter;
            _triggerObserver.TriggerExit += OnDamageTriggerExit;
        }
        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnDamageTriggerEnter;
            _triggerObserver.TriggerExit -= OnDamageTriggerExit;
        }

        private void OnDamageTriggerExit(Collider2D other)
        {
            MakeDamage(other);
        }

        private void OnDamageTriggerEnter(Collider2D other)
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

        private IEnumerator Damage(Component player)
        {
            yield return new WaitForSeconds(_trapStaticData.Cooldown);
            player.GetComponent<Player>().TakeDamage(_trapStaticData.Damage);
        }
    }
}
