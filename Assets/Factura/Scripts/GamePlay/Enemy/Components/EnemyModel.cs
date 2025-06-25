using System;
using UnityEngine;

namespace Game
{
    public class EnemyModel
    {
        public event Action<float> OnChangeHealth;
        public event Action<float> OnChangeSpeed;
        public event Action OnDeath;

        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                _isAlive = value;
                if (!_isAlive)
                    OnDeath?.Invoke();
            }
        }
        
        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0f, _config.MaxHealth);
                OnChangeHealth?.Invoke(_health);
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

        public EnemyModel(EnemyConfig config)
        {
            _config = config;
            
            Speed = config.IdleSpeed;
            Health = config.MaxHealth;
        }
        
        private readonly EnemyConfig _config;

        private float _health;
        private float _speed;
        private bool _isAlive;
    }
}
