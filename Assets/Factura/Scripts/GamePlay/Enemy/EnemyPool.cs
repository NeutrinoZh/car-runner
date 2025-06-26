using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyPool : MonoMemoryPool<Vector3, Enemy>
    {
        protected override void Reinitialize(Vector3 position, Enemy enemy)
        {
            enemy.Init(position);
        }
    }
}