using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game {
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private FollowingCamera _followingCamera;
        
        public override void InstallBindings()
        {
            Container
                .Bind<InputControls>()
                .AsSingle();

            Container
                .Bind<FollowingCamera>()
                .FromInstance(_followingCamera);
            
            Container
                .BindFactory<Player, Player.Factory>()
                .FromComponentInNewPrefab(_playerPrefab)
                .UnderTransformGroup(TransformGroups.GamePlayGroup);

            Container
                .Bind<Transform>()
                .WithId(Player.SpawnTransformId)
                .FromInstance(_playerSpawnPoint);
            
            InstallGsm();
        }

        private void InstallGsm()
        {
            var gsm = new GameStateMachine(new Dictionary<Type, IGameState>()
            {
                { typeof(BootstrapState), Container.Instantiate<BootstrapState>() },
            });

            Container.Bind<GameStateMachine>().FromInstance(gsm).AsSingle();
            gsm.Enter<BootstrapState>();
        }
    }
}