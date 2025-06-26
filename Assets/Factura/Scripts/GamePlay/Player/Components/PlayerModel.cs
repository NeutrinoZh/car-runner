using System;
using UnityEngine;

namespace Game
{
    public class PlayerModel
    {
        public event Action<float> OnChangeHealth;
        public event Action<float> OnChangeSpeed;
        public event Action OnDeath;
        
        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0f, _config.MaxHealth);
                OnChangeHealth?.Invoke(_health);

                if (_health > 0 || !_isAlive)
                    return;
                
                _isAlive = false;
                OnDeath?.Invoke();
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                OnChangeSpeed?.Invoke(_speed);
            }
        }

        public float FireDelay => _config.FireDelay;
        public bool IsAlive => _health > 0;
        
        public PlayerModel(PlayerConfig config)
        {
            _config = config;
            
            _isAlive = true;
            Speed = config.BaseSpeed;
            Health = config.MaxHealth;
        }
        
        private readonly PlayerConfig _config;

        private float _health;
        private float _speed;
        private bool _isAlive;
    }
}