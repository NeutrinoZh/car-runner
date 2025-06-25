using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _enemyConfig;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(transform);
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig);

            Container.BindInterfacesAndSelfTo<EnemyModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyDamagable>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EnemyIdle>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAngry>().AsSingle();
            Container.BindInterfacesAndSelfTo<Animator>().FromInstance(GetComponentInChildren<Animator>());
            Container.Bind<Dictionary<System.Type, IEnemyState>>().FromMethod(
                ctx => new ()
                {
                    { typeof(EnemyIdle), ctx.Container.Resolve<EnemyIdle>() },
                    { typeof(EnemyAngry), ctx.Container.Resolve<EnemyAngry>() }
                })
                .WhenInjectedInto<EnemySM>();
            Container.BindInterfacesAndSelfTo<EnemySM>().AsSingle().NonLazy();
        }

        public override void Start()
        {
            base.Start();
            Container.Resolve<EnemySM>().Enter<EnemyIdle>();
        }
    }
}