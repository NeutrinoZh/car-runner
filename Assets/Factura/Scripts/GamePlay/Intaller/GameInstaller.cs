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
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayerSpawnedSignal>();

            Container.Bind<PlayersList>().AsSingle();
            
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
            Container
                .Bind<Dictionary<System.Type, IGameState>>()
                .FromMethod(ctx => new()
                {
                    { typeof(BootstrapState), Container.Instantiate<BootstrapState>() },
                })
                .WhenInjectedInto<GameSM>();
            Container.Bind<GameSM>().AsSingle();
        }

        public override void Start()
        {
            base.Start();
            Container.Resolve<GameSM>().Enter<BootstrapState>();
        }
    }
}