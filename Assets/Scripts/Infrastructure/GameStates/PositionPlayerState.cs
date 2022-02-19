using System;
using System.Threading.Tasks;
using Cinemachine;
using Common;
using PlayerScripts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class PositionPlayerState : IPayloadState<Player>
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly Camera _camera;
        private ICinemachineCamera _virtualCamera;
        private Player _player;

        public PositionPlayerState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _camera = Camera.main;
        }
        public void Enter(Player player)
        {
            _player = player;
            PositionPlayer(player, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
        }

        private async void PositionPlayer(Component player, Action onLoaded)
        {
            PlayerSpawnPoint spawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();
            Transform playerTransform = player.transform;
            playerTransform.position = spawnPoint.transform.position;
            _virtualCamera = await GetVCamera();
            _virtualCamera.VirtualCameraGameObject.SetActive(false);
            _virtualCamera.VirtualCameraGameObject.transform.position = playerTransform.position;
            _virtualCamera.Follow = playerTransform;
            _virtualCamera.LookAt = playerTransform;
            _virtualCamera.VirtualCameraGameObject.SetActive(true);
            onLoaded?.Invoke();
        }

        private void OnLoaded()
        {
            
            _stateMachine.Enter<CreateCrowbarState, Player>(_player);
        }
        
        private async Task<ICinemachineCamera> GetVCamera()
        {
            await Task.Delay(100);
            return _camera.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        }
    }
}