using System;
using GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    static PlayerInputController _instance;
    
    [SerializeField] GameEvent onPlayerStateChanged;
    [SerializeField] float moveSpeed = 5f;
    
    public static Vector2 MovementDirection;
    DefaultInputActions _inputActions;
    public static PlayerState CurrentState { get; private set; } = PlayerState.Idle;

    public enum PlayerState
    {
        Idle,
        Walking,
        Shooting
    }

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

    void Update()
    {
        if(CurrentState == PlayerState.Walking)
            transform.Translate(MovementDirection * (moveSpeed * Time.deltaTime), Space.World);
    }
    
    void SetupInputActions()
    {
        _inputActions = new DefaultInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += StopMoving;

        _inputActions.Player.Fire.performed += StartFire;
    }

    void StartFire(InputAction.CallbackContext obj)
    {
        ChangePlayerState(PlayerState.Shooting);
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
        _instance.onPlayerStateChanged.Raise();
    }
}
