using System;
using System.Collections.Generic;
using Common;
using Input;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    [RequireComponent(typeof(StateMachine))]
    public class Crowbar : MonoBehaviour
    {
        private float _playerSpeed = 50f;
        private StateMachine _stateMachine;
        private DevicesInput _input;
        private Player _player;
        private Rigidbody2D _rigidbody;
        private PlayerSurfaceNormal _playerSurfaceNormal;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _playerSurfaceNormal = new PlayerSurfaceNormal(_player);
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

        private void OnDrawGizmos()
        {
            var position = _player.transform.position;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position, position + _playerSurfaceNormal.Value().normalized);
            Gizmos.color = Color.yellow;
            var direction = new Vector3(_input.Direction, position.y, position.z);
            Gizmos.DrawLine(position, position + Project(direction).normalized);
        }

        private void Move()
        {
            if (_input.Direction == 0) return;
            var position = _player.transform.position;
            var direction = new Vector3(_input.Direction, 0, 0);
            var directionAlongSurface = Project(direction);
            var offset = directionAlongSurface * (_playerSpeed * Time.deltaTime);
            _rigidbody.MovePosition(position + offset);
        }

        private Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction,
                _playerSurfaceNormal.Value()) * _playerSurfaceNormal.Value();
        }
    }
}