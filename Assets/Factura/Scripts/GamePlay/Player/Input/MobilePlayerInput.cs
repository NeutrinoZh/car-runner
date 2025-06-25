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
            _controls.PlayerInput.Direction.performed += PerformedDirection;
            _controls.PlayerInput.Direction.canceled += CanceledDirection;
        }

        private void OnDestroy()
        {
            _controls.PlayerInput.Direction.performed -= PerformedDirection;
            _controls.PlayerInput.Direction.canceled -= CanceledDirection;
        }

        private void PerformedDirection(InputAction.CallbackContext callbackContext)
        {
            _tapped = true;
            _tapPosition = callbackContext.ReadValue<Vector2>();
        }

        private void CanceledDirection(InputAction.CallbackContext callbackContext)
        {
            _tapped = false;
        }
    }
}