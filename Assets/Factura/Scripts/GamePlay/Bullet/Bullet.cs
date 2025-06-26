using UnityEngine;
using Zenject;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        
        public float Damage => _damage;
        
        private Vector3 _direction;
        private BulletPool _pool;

        [Inject]
        public void Construct(BulletPool pool)
        {
            _pool = pool;
        }
        
        public void Init(Vector3 position, Vector3 direction)
        {
            gameObject.SetActive(true);
            _direction = direction;
            transform.position = position;
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
            _pool.Despawn(this);
        }

        private void Update()
        {
            transform.position += _direction * (_speed * Time.deltaTime);
        }
    }
}