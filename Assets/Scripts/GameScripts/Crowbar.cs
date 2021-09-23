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
            Gizmos.color = Color.white;
            Gizmos.DrawLine(_player.transform.position, _player.transform.position + _playerSurfaceNormal.Value().normalized);
            Gizmos.color = Color.yellow;
            var direction = new Vector3(_input.Direction, _player.transform.position.y, _player.transform.position.z);
            Gizmos.DrawLine(_player.transform.position, _player.transform.position + Project(direction).normalized);
        }

        private void Move()
        {
            if (_playerSurfaceNormal.Value() == Vector3.zero) return;
            var direction = new Vector3(_input.Direction, 0, _player.transform.position.z);
            var directionAlongSurface = Project(direction.normalized);
            var offset = directionAlongSurface * (_playerSpeed * Time.deltaTime);
            _rigidbody.MovePosition(_player.transform.position + offset);
        }

        private Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction,
                _playerSurfaceNormal.Value()) * _playerSurfaceNormal.Value();
        }
    }
}