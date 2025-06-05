using System;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        public static PlayerState CurrentState { get; private set; } = PlayerState.Idle;
        public static DefaultInputActions InputActions { get; private set; }
        public static Transform PlayerTransform { get; private set; }
        public static Vector2 MovementDirection { get; private set; }
        
        static PlayerInputController _instance;


        [SerializeField] List<InputActionReference> playersInputActions;
        
        [SerializeField] GameEvent onPlayerStateChanged;
        [SerializeField] float moveSpeed = 5f;
        

        void Awake()
        {
            if (null != _instance)
                Destroy(_instance);
        
            _instance = this;
            PlayerTransform = _instance.transform;
            SetupInputActions();
        }
        
        void FixedUpdate()
        {
            if(CurrentState == PlayerState.Walking)
                transform.Translate(MovementDirection * (moveSpeed * Time.fixedDeltaTime), Space.World);
        }

        void OnDestroy()
        {
            InputActions.Player.Move.performed -= OnMove;
            InputActions.Player.Move.canceled -= StopMoving;
        }

        void SetupInputActions()
        {
            InputActions = new DefaultInputActions();
            ToggleInputActions(true);
            InputActions.Player.Move.performed += OnMove;
            InputActions.Player.Move.canceled += StopMoving;
        }

        public static void ToggleInputActions(bool enable)
        {
            if (enable)
            {
                InputActions.Player.Enable();
                for (int i = 0; i < _instance.playersInputActions.Count; i++)
                {
                    _instance.playersInputActions[i].action.Enable();
                }
                return;
            }
            
            InputActions.Player.Disable();
            for (int i = 0; i < _instance.playersInputActions.Count; i++)
            {
                _instance.playersInputActions[i].action.Disable();
            }
        }

        void OnMove(InputAction.CallbackContext obj)
        {
            MovementDirection = obj.ReadValue<Vector2>().normalized;
            ChangePlayerState(PlayerState.Walking);
        }

        void StopMoving(InputAction.CallbackContext obj)
        {
            MovementDirection = Vector2.zero;
        
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
        }
    }
    
    public enum PlayerState
    {
        Idle,
        Walking,
        Attack,
        Shooting,
        UsingItem,
        Dialogue
    }
}
