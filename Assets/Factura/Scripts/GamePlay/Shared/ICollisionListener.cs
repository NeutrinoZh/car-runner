using UnityEngine;

namespace Game
{
    public interface ICollisionListener
    {
        public void Collision(Transform other);
    }
}