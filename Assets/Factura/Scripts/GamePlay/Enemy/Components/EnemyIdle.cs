using UnityEngine;

namespace Game
{
    public class EnemyIdle : IEnemyState
    {
        public EnemySM SM { get; set; }
        
        private static readonly int k_idleAnimation = Animator.StringToHash("Idle");

        private const float k_angrySqrRadius = 100f;
        private const float k_walkRadius = 5f;
        private const float k_minSqrDistance = 0.5f;
        private const float k_rotationSpeed = 5f;
        
        private readonly EnemyModel _model;
        private readonly EnemyConfig _config;
        private readonly Transform _transform;
        private readonly PlayersList _playersList;
        private readonly Animator _animator;
        
        private Vector3 _target;
        
        public EnemyIdle(
            Transform transform,
            EnemyConfig config, 
            EnemyModel model,
            PlayersList playersList,
            Animator animator)
        {
            _playersList = playersList;
            _transform = transform;
            _animator = animator;
            _config = config;
            _model = model;
        }
        
        public void Enter()
        {
            _animator.SetBool(k_idleAnimation, true);
            _model.Speed = _config.IdleSpeed;
            RandomTarget();
        }

        public void Tick()
        {
            var offset = _target - _transform.position;
            if (offset.sqrMagnitude < k_minSqrDistance)
            {
                RandomTarget();
                return;
            }
            
            _transform.position += offset.normalized * (_model.Speed * Time.deltaTime);

            var targetRotation = Quaternion.LookRotation(offset.normalized);
            _transform.rotation = Quaternion.Lerp(
                _transform.rotation, 
                targetRotation, 
                k_rotationSpeed * Time.deltaTime);

            if (!_playersList.MainPlayer)
                return;
            
            var offsetToPlayer = _transform.position - _playersList.MainPlayer.transform.position;
            if (offsetToPlayer.sqrMagnitude < k_angrySqrRadius) 
                SM.Enter<EnemyAngry>();
        }

        public void Exit()
        {
            _animator.SetBool(k_idleAnimation, false);   
        }

        private void RandomTarget()
        {
            var randomDirection = Random.insideUnitCircle * k_walkRadius;
            _target = _transform.position + new Vector3(randomDirection.x, 0, randomDirection.y);
        }
    }
}