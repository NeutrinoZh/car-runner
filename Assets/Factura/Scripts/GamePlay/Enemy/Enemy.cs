using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        private const int k_dieDelay = 1000;
        
        private List<ICollisionListener> _listOfCollisionListeners;
        private EnemyPool _pool;
        
        private EnemySM _enemySm;
        private EnemyModel _model;
        private VisualEffect _visualEffect;
        
        private Transform _mesh;
        private BoxCollider _collider;
        
        [Inject]
        public void Construct(EnemyModel model, EnemyPool pool, EnemySM enemySm, List<ICollisionListener> listOfListeners)
        {
            _pool = pool;
            _model = model;
            _enemySm = enemySm;
            _listOfCollisionListeners = listOfListeners;
            
            _mesh = transform.GetChild(0);
            _collider = transform.GetComponent<BoxCollider>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            foreach (var listener in _listOfCollisionListeners)
                listener.Collision(other.transform);
        }

        private void Awake()
        {
            _visualEffect = GetComponentInChildren<VisualEffect>();
            _model.OnDeath += StartDieRoutine;
        }

        private void OnDestroy()
        {
            _model.OnDeath -= StartDieRoutine;
        }
        
        public void Init(Vector3 position)
        {
            _model.Reset();
            gameObject.SetActive(true);
            _collider.enabled = true;
            _mesh.gameObject.SetActive(true);
            transform.position = position;
            _visualEffect.Stop();
            
            _enemySm.Enter<EnemyIdle>();
        }

        private void StartDieRoutine()
        {
            _collider.enabled = false;
            _mesh.gameObject.SetActive(false);
            _ = DieHandle();
        }

        private async Task DieHandle()
        {
            await Task.Delay(k_dieDelay);
            Die();
        }
        
        private void Die()
        {
            gameObject.SetActive(false);
            _pool.Despawn(this);
            _pool.OnDie?.Invoke();
        }
    }
}