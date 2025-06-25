using UnityEngine;
using Zenject;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Player> { }
        
        public const string SpawnTransformId = "PlayerSpawn";
    }
}