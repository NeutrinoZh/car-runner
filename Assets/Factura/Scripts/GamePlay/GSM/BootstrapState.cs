using UnityEngine;
using Zenject;

namespace Game
{
    public class BootstrapState : IGameState
    {
        private readonly FollowingCamera _followingCamera;
        private readonly Player.Factory _playerFactory;
        private readonly Transform _playerSpawnTransform;
        
        public BootstrapState(
            FollowingCamera camera,
            Player.Factory playerFactory,
            [Inject(Id=Player.SpawnTransformId)] Transform playerSpawnTransform
            )
        {
            _playerFactory = playerFactory;
            _playerSpawnTransform = playerSpawnTransform;
            _followingCamera = camera;
        }
        
        public void Enter()
        {
            var player = _playerFactory.Create();
            player.transform.position = _playerSpawnTransform.position;
            
            _followingCamera.Target = player.transform;
            _followingCamera.Follow = true;
        }

        public void Exit()
        {
            
        }
    }
}