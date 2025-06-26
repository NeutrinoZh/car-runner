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

        private PlayerModel _playerModel;
        
        [Inject]
        public void Construct(PlayerModel model)
        {
            _playerModel = model;
        }
        
        public void Damage(float damage)
        {
            _playerModel.Health -= damage;
        }
    }
}