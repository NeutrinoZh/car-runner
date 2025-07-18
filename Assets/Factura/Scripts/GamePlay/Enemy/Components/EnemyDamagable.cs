﻿using UnityEngine;
using UnityEngine.VFX;

namespace Game
{
    public class EnemyDamagable : ICollisionListener
    {
        private static readonly int k_directionParamId = Shader.PropertyToID("Direction");
        
        private readonly Transform _transform;
        private readonly EnemyModel _model;

        private readonly VisualEffect _visualEffect;
        
        public EnemyDamagable(Transform transform, EnemyModel model)
        {
            _model = model;
            _transform = transform;
            _visualEffect = transform.GetComponentInChildren<VisualEffect>();
        }

        public void Collision(Transform other)
        {
            var normal = _transform.position - other.position;
            _visualEffect.SetVector3(k_directionParamId, normal);
            _visualEffect.Play();

            if (other.TryGetComponent(out Player player))
            {
                player.Damage(_model.Damage);
                _model.Health = 0;
            }

            if (other.TryGetComponent(out Bullet bullet))
            {
                _model.Health -= bullet.Damage;
                bullet.Destroy();
            }
            
            if (_model.Health <= 0) 
                _model.IsAlive = false;
        }
    }
}