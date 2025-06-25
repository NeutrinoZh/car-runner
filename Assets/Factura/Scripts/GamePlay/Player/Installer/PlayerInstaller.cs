using Game.Input;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(transform);
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
            Container.Bind<IPlayerInput>().FromInstance(GetComponent<IPlayerInput>());
            
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().NonLazy();
            
            Container.Bind<Player>().AsSingle();
        }
    }
}