using System;
using System.Collections.Generic;
using Common;
using Input;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

namespace GameScripts
{
    [RequireComponent(typeof(StateMachine))]
    public class Crowbar : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private DevicesInput _input;
        private Player _player;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
        }

        private void Start()
        {
            _rigidbody = _player.GetComponent<Rigidbody2D>();
            _stateMachine = GetComponent<StateMachine>();
            _input = GetComponent<DevicesInput>();
            
            var playerGameObject = _player.gameObject;
            _stateMachine.Initialize(new Dictionary<Type, BaseState>
            {
                {typeof(IdleState), new IdleState(playerGameObject, _stateMachine)},
                {typeof(WalkState), new WalkState(playerGameObject, _stateMachine)}
            });
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
           _rigidbody.MovePosition(_player.transform.position + new Vector3(_input.Direction,0,0) * 20 * Time.deltaTime);
        }
    }
}