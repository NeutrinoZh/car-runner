using UnityEngine;
using Zenject;

namespace Game
{
    public class BulletPool : MonoMemoryPool<Vector3, Vector3, Bullet>
    {
        protected override void Reinitialize(Vector3 position, Vector3 direction, Bullet bullet)
        {
            bullet.Init(position, direction);
        }
    }
}