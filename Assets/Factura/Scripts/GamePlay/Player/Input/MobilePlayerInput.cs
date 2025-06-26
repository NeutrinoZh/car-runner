using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Input
{
    public class MobilePlayerInput : MonoBehaviour, IPlayerInput
    {
        public Vector2 Direction => _tapPosition;
        public bool IsShooting => _tapped;
        
        private Vector2 _tapPosition;
        private bool _tapped;

        private InputControls _controls;
        
        [Inject]
        public void Construct(InputControls inputControls)
        {
            _controls = inputControls;
        }

        private void Start()
        {
            _controls.Enable();
        }

        private void OnDestroy()
        {
            _controls.Disable();
        }
        
        private void Update()
        {
            _tapPosition = _controls.PlayerInput.Direction.ReadValue<Vector2>();
            _tapped = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0;
        }
    }
}