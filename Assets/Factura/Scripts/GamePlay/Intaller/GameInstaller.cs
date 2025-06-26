using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game {
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _gamePlayGroup;
        [SerializeField] private Transform _enemyGroup;
        [SerializeField] private Transform _bulletGroup;
        
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private FollowingCamera _followingCamera;
        [SerializeField] private MenuScreens _menuScreens;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayerSpawnedSignal>();

            Container.Bind<GameSM>().AsSingle();
            
            Container.Bind<PlayersList>().AsSingle();
            Container.Bind<InputControls>().AsSingle();
            Container.Bind<MenuScreens>().FromInstance(_menuScreens);
            Container.Bind<FollowingCamera>().FromInstance(_followingCamera);
            
            Container
                .BindFactory<Player, Player.Factory>()
                .FromComponentInNewPrefab(_playerPrefab)
                .UnderTransform(_gamePlayGroup);

            Container
                .Bind<Transform>()
                .WithId(Player.SpawnTransformId)
                .FromInstance(_playerSpawnPoint);

            Container
                .BindMemoryPool<Enemy, EnemyPool>()
                .WithInitialSize(100)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransform(_enemyGroup);
            
            Container
                .BindMemoryPool<Bullet, BulletPool>()
                .WithInitialSize(30)
                .FromComponentInNewPrefab(_bulletPrefab)
                .UnderTransform(_bulletGroup);
            
            Container
                .BindInterfacesAndSelfTo<EnemySpawner>()
                .AsSingle()
                .NonLazy();
        }
        
        public override void Start()
        {
            base.Start();

            var gameSm = Container.Resolve<GameSM>();
            gameSm.SetStates(new()
            {
                { typeof(BootstrapState), Container.Instantiate<BootstrapState>() },
                { typeof(PauseState), Container.Instantiate<PauseState>() },
                { typeof(PlayState), Container.Instantiate<PlayState>() },
            });
            
            gameSm.Enter<BootstrapState>();
        }
    }
}