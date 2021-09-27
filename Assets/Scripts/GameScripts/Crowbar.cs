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
        private PlayerSurfaceNormal _playerSurfaceNormal;
        private PlayerView _playerView;
        private FlipView _flipView;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _playerSurfaceNormal = new PlayerSurfaceNormal(_player);
            _playerView = _player.GetComponentInChildren<PlayerView>();
            _flipView = new FlipView(_playerView);
        }

        private void Start()
        {
            _rigidbody = _player.GetComponent<Rigidbody2D>();
            _stateMachine = GetComponent<StateMachine>();
            _input = GetComponent<DevicesInput>();
            
            var player = _player.gameObject;
            _stateMachine.Initialize(new Dictionary<Type, BaseState>
            {
                {typeof(IdleState), new IdleState(player)},
                {typeof(WalkState), new WalkState(player)},
                {typeof(JumpState), new JumpState(player)}
            });
        }

        private void Update()
        {
            FLip();
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
        }

        private void OnDrawGizmos()
        {
            var position = _player.transform.position;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position, position + _playerSurfaceNormal.Value());
            Gizmos.color = Color.yellow;
            var direction = new Vector3(_input.Direction, 0, 0);
            Gizmos.DrawLine(position, position + Project(direction));
        }

        private void Move()
        {
            if(_playerSurfaceNormal.Value() != Vector3.zero)
            {
                _rigidbody.AddForce(CalculateDirection() * _player.Speed, ForceMode2D.Impulse);
                var maxVelocity = _player.MaxVelocity;
                var velocity = new Vector2(
                    Mathf.Clamp(_rigidbody.velocity.x, -maxVelocity, maxVelocity),
                    _rigidbody.velocity.y
                );
                _rigidbody.velocity = Math.Abs(velocity.x) > _player.XMoveDamping ? velocity : Vector2.zero;
            }
        }

        private void Jump()
        {
            if(_playerSurfaceNormal.Value() != Vector3.zero)
            {
                var jumpForce = new Vector2(0, _input.Jump) * _player.JumpForce;
                _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }

        private Vector3 Project(Vector3 direction)
        {
            return direction 
                   - Vector3.Dot(direction,_playerSurfaceNormal.Value()) 
                   * _playerSurfaceNormal.Value();
        }

        private Vector3 CalculateDirection()
        {
            var direction = new Vector3(_input.Direction, 0, 0);
            var directionAlongSurface = Project(direction);
            return directionAlongSurface;
        }

        private void FLip()
        {
            _flipView.FLippingPlayerView(_input.Direction);
        }
    }
}