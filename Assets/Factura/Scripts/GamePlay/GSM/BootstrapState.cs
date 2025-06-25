using UnityEngine;
using Zenject;

namespace Game
{
    public class BootstrapState : IGameState
    {
        private readonly SignalBus _signalBus;
        
        private readonly PlayersList _playersList;
        private readonly Player.Factory _playerFactory;
        private readonly Transform _playerSpawnTransform;
        private readonly FollowingCamera _followingCamera;
        
        public BootstrapState(
            SignalBus signalBus,
            FollowingCamera camera,
            PlayersList playersList,
            Player.Factory playerFactory,
            [Inject(Id=Player.SpawnTransformId)] Transform playerSpawnTransform
            )
        {
            _signalBus = signalBus;
            _followingCamera = camera;
            _playersList = playersList;
            _playerFactory = playerFactory;
            _playerSpawnTransform = playerSpawnTransform;
        }
        
        public void Enter()
        {
            var player = _playerFactory.Create();
            player.transform.position = _playerSpawnTransform.position;
            
            _playersList.MainPlayer = player;
            _signalBus.Fire(new PlayerSpawnedSignal{ Player = player });
            
            _followingCamera.Target = player.transform;
            _followingCamera.Follow = true;
        }

        public void Exit()
        {
            
        }
    }
}