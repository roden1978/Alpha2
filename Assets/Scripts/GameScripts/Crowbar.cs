using System;
using System.Collections.Generic;
using Common;
using Input;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

namespace GameScripts
{
    [RequireComponent(typeof(StateMashine))]
    public class Crowbar : MonoBehaviour
    {
        [SerializeField] private StateMashine _stateMashine;
        [SerializeField] private DevicesInput _input;
        private Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
            _stateMashine.Initialize(new Dictionary<Type, BaseState>()
            {
                {typeof(IdleState), new IdleState(_player.gameObject)}
            });
            
        }
    }
}