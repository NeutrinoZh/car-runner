using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerMovement : ITickable
    {
        private readonly Vector3 k_direction = Vector3.back; 
        
        private readonly Transform _transform;
        private readonly PlayerModel _playerModel;
        
        public PlayerMovement(PlayerModel playerModel, Transform transform)
        {
            _transform = transform;
            _playerModel = playerModel;
        }
        
        public void Tick()
        {
            _transform.position += Time.deltaTime * _playerModel.Speed * k_direction;
        }
    }
}