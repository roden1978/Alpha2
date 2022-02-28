using System;
using UnityEngine;

namespace EnemyScripts
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AttackShooting attackShooting;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnAggroTriggerEnter;
            _triggerObserver.TriggerExit += OnAggroTriggerExit;
            SwitchAttackOff();
        }

        private void OnAggroTriggerEnter(Collider2D obj) => 
            SwitchAttackOn();

        private void OnAggroTriggerExit(Collider2D obj) => 
            SwitchAttackOff();

        private void SwitchAttackOn() => 
            attackShooting.enabled = true;

        private void SwitchAttackOff() => 
            attackShooting.enabled = false;
    }
}