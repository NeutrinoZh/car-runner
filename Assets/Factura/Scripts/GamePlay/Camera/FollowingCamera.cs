using UnityEngine;

namespace Game
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform Target { get; set; }
        public bool Follow { get; set; }
        
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private Quaternion _targetRotation;
        
        [SerializeField] private float _cameraSpeed;
        [SerializeField] private float _rotationSpeed;
        
        private void Update()
        {
            if (!Follow || !Target)
                return;
            
            var position = Vector3.Lerp(
                transform.position, 
                Target.position + _cameraOffset, 
                _cameraSpeed * Time.deltaTime
                );
            
            transform.position = position;   
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                _targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }
}