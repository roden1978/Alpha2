using System;
using System.Resources;
using System.Threading.Tasks;
using Cinemachine;
using Common;
using PlayerScripts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class PositionPlayerState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly Camera _camera;
        private ICinemachineCamera _virtualCamera;

        public PositionPlayerState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _camera = Camera.main;
        }
        public void Enter(StatesPayload statesPayload)
        {
            PositionPlayer(statesPayload, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
        }

        private async void PositionPlayer(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            PlayerSpawnPoint spawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();
            Transform playerTransform = statesPayload.Player.transform;
            playerTransform.position = spawnPoint.transform.position;
            _virtualCamera = await GetVCamera();
            _virtualCamera.VirtualCameraGameObject.SetActive(false);
            _virtualCamera.VirtualCameraGameObject.transform.position = playerTransform.position;
            _virtualCamera.Follow = playerTransform;
            _virtualCamera.LookAt = playerTransform;
            _virtualCamera.VirtualCameraGameObject.SetActive(true);
            onLoaded?.Invoke(statesPayload);
        }

        private void OnLoaded(StatesPayload statesPayload)
        {
            _stateMachine.Enter<CreateCrowbarState, StatesPayload>(statesPayload);
        }
        
        private async Task<ICinemachineCamera> GetVCamera()
        {
            await Task.Delay(100);
            return _camera.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        }
    }
}