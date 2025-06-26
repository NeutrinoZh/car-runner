using UnityEngine;
using Zenject;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Vector3 _direction;
        private BulletPool _pool;

        [Inject]
        public void Construct(BulletPool pool)
        {
            _pool = pool;
        }
        
        public void Init(Vector3 position, Vector3 direction)
        {
            _direction = direction;
            transform.position = position;
        }

        private void Update()
        {
            transform.position += _direction * (_speed * Time.deltaTime);
        }
    }
}