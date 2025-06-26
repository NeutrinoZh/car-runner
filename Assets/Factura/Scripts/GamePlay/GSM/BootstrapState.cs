using UnityEngine;
using Zenject;

namespace Game
{
    public class BootstrapState : IGameState
    {
        public GameSM GameSM { get; set; }
        
        private readonly SignalBus _signalBus;
        
        private readonly PlayersList _playersList;
        private readonly Player.Factory _playerFactory;
        private readonly Transform _playerSpawnTransform;
        
        public BootstrapState(
            SignalBus signalBus,
            PlayersList playersList,
            Player.Factory playerFactory,
            [Inject(Id=Player.SpawnTransformId)] Transform playerSpawnTransform
            )
        {
            _signalBus = signalBus;
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
            
            GameSM.Enter<PauseState>();
        }

        public void Exit()
        {
            
        }
    }
}