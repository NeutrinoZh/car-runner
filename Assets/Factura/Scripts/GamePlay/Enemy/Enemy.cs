using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private List<ICollisionListener> _listOfCollisionListeners;

        [Inject]
        public void Construct(List<ICollisionListener> listOfListeners)
        {
            _listOfCollisionListeners = listOfListeners;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            foreach (var listener in _listOfCollisionListeners)
                listener.Collision(other.transform);
        }
    }
}