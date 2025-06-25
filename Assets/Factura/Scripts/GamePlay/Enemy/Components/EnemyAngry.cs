using UnityEngine;
    
namespace Game
{
    public class EnemyAngry : IEnemyState
    {
        public EnemySM SM { get; set; }
        
        private static readonly int k_runAnimationTrigger = Animator.StringToHash("Run");
        private const float k_rotationSpeed = 5f;
        
        private readonly Transform _transform;
        private readonly EnemyConfig _config;
        private readonly EnemyModel _model;
        private readonly PlayersList _playersList;
        private readonly Animator _animator;
        
        public EnemyAngry(
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
            _model.Speed = _config.AngrySpeed;
            _animator.SetTrigger(k_runAnimationTrigger);
        }

        public void Tick()
        {
            if (!_playersList.MainPlayer)
                return;
            
            var offset = _playersList.MainPlayer.transform.position - _transform.position;
            
            _transform.position += offset.normalized * (_model.Speed * Time.deltaTime);
            
            var targetRotation = Quaternion.LookRotation(offset.normalized);
            _transform.rotation = Quaternion.Lerp(
                _transform.rotation, 
                targetRotation, 
                k_rotationSpeed * Time.deltaTime);
        }

        public void Exit()
        {
            
        }
    }
}