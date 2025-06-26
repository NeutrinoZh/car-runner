using UnityEngine;
using Zenject;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Player> { }
        
        public const string SpawnTransformId = "PlayerSpawn";
        public const string PlayerTransformId = "Player";
        public const string TurretTransformId = "PlayerTurret";
    }
}