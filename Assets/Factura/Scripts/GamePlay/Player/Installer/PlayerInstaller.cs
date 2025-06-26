using Game.Input;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _turret;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(transform);
            Container.Bind<Transform>().WithId(Player.TurretTransformId).FromInstance(_turret);
            
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
            Container.Bind<IPlayerInput>().FromInstance(GetComponent<IPlayerInput>());
            Container.Bind<Animator>().FromInstance(GetComponent<Animator>());
            
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerTurret>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimation>().AsSingle().NonLazy();
            
            Container.Bind<Player>().AsSingle();
        }
    }
}