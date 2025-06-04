using System;
using GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        static PlayerInputController _instance;
    
        [SerializeField] Rigidbody2D playerRigidbody;
        [SerializeField] GameEvent onPlayerStateChanged;
        [SerializeField] float moveSpeed = 5f;
    
        public static Vector2 MovementDirection;
        DefaultInputActions _inputActions;
        public static PlayerState CurrentState { get; private set; } = PlayerState.Idle;

        private void Awake()
        {
            if (null != _instance)
                Destroy(_instance);
        
            _instance = this;
        }

        void Start()
        {
            SetupInputActions();
        }

        void FixedUpdate()
        {
            //if(CurrentState == PlayerState.Walking)
                
        }

        void OnDestroy()
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= StopMoving;
        }

        void SetupInputActions()
        {
            _inputActions = new DefaultInputActions();
            ToggleInputActions(true);
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += StopMoving;
        }

        void ToggleInputActions(bool enable)
        {
            if (enable)
            {
                _inputActions.Player.Enable();
                return;
            }
            
            _inputActions.Player.Disable();
        }

        void OnMove(InputAction.CallbackContext obj)
        {
            MovementDirection = obj.ReadValue<Vector2>().normalized;
            playerRigidbody.linearVelocity = MovementDirection * (moveSpeed * Time.fixedDeltaTime);
            ChangePlayerState(PlayerState.Walking);
        }

        void StopMoving(InputAction.CallbackContext obj)
        {
            MovementDirection = Vector2.zero;
            playerRigidbody.linearVelocity = Vector2.zero;
        
            if(CurrentState == PlayerState.Walking)
                ChangePlayerState(PlayerState.Idle);
        }
    
        public static void ChangePlayerState(PlayerState newState)
        {
            CurrentState = newState;
            Debug.Log($"Player state changed to: {CurrentState}");
            _instance.OnChangePlayerState();
        }

        void OnChangePlayerState()
        {
            onPlayerStateChanged.Raise();
            bool canInput = (CurrentState != PlayerState.Dialogue 
                             && CurrentState != PlayerState.UsingItem);
            
            ToggleInputActions(canInput);
        }
    }
    
    public enum PlayerState
    {
        Idle,
        Walking,
        Attack,
        Shooting,
        UsingItem,
        Dialogue,
    }
}
