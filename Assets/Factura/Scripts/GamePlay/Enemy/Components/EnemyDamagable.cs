using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Game
{
    public class EnemyDamagable : ICollisionListener
    {
        private static readonly int k_directionParamId = Shader.PropertyToID("Direction");
        
        private readonly Transform _transform;
        private readonly EnemyModel _model;

        private readonly Transform _mesh;
        private readonly BoxCollider _collider;
        private readonly VisualEffect _visualEffect;
        
        public EnemyDamagable(Transform transform, EnemyModel model)
        {
            _model = model;
            _transform = transform;
            _mesh = transform.GetChild(0);
            _collider = transform.GetComponent<BoxCollider>();
            _visualEffect = transform.GetComponentInChildren<VisualEffect>();

            _model.OnDeath += DieHandle;
        }

        ~EnemyDamagable()
        {
            _model.OnDeath -= DieHandle;
        }

        public void Collision(Transform other)
        {
            var normal = _transform.position - other.position;
            _visualEffect.SetVector3(k_directionParamId, normal);
            _visualEffect.Play();
            
            if (other.TryGetComponent(out Player player))
                _model.IsAlive = false;
        }

        private void DieHandle()
        {
            _collider.enabled = false;
            _mesh.gameObject.SetActive(false);
        }
    }
}