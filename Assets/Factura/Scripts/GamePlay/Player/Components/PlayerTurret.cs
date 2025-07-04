﻿using System.Threading.Tasks;
using Game.Input;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerTurret : IInitializable, ITickable
    {
        private const int k_zOffset = 4;
        
        private readonly Transform _turret;
        private readonly IPlayerInput _playerInput;
        private readonly Camera _camera;
        private readonly BulletPool _bulletPool;
        private readonly PlayerModel _playerModel;
        private readonly GameSM _gameSm;

        private Vector3 _direction;
        
        public PlayerTurret(
            IPlayerInput playerInput,
            BulletPool bulletPool,
            PlayerModel playerModel,
            GameSM gameSm,
            [Inject(Id = Player.TurretTransformId)] Transform turret
        )
        {
            _playerInput = playerInput;
            _playerModel = playerModel;
            _bulletPool = bulletPool;
            _camera = Camera.main;
            _turret = turret;        
            _gameSm = gameSm;
        }
        
        public void Initialize()
        {
            _ = FireRoutine();
        }
        
        public void Tick()
        {
            if (!_playerModel.IsAlive)
                return;
            
            var screenDirection = new Vector3(_playerInput.Direction.x, _playerInput.Direction.y, 15f);
            var worldPosition = _camera.ScreenToWorldPoint(screenDirection);
            worldPosition.z -= k_zOffset;
            
            _direction = worldPosition - _turret.position;
            _direction.y = 0;
            
            if (_direction.sqrMagnitude > 0.001f) 
            {
                var targetRotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
                _turret.rotation = targetRotation;
            }
        }

        private async Task FireRoutine()
        {
            await Task.Delay(100);

            while (_playerModel.IsAlive)
            {
                if (!_gameSm.IsState<PlayState>())
                {
                    await Task.Delay(100);
                    continue;
                }

                float delay = 100;
                
                if (_playerInput.IsShooting)
                {
                    _bulletPool.Spawn(_turret.position, _turret.forward);   
                    delay = Mathf.Clamp(_playerModel.FireDelay, 100f, 2000f);
                }
                    
                await Task.Delay((int)(delay * Time.timeScale));
            }
        }
    }
}