using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerAnimController animController;
    [SerializeField] float moveSpeed = 5f;
    
    Vector2 _movementDirection;
    DefaultInputActions _inputActions;

    void Start()
    {
        SetupInputActions();
    }

    void Update()
    {
        transform.Translate(_movementDirection * (moveSpeed * Time.deltaTime), Space.World);
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
        StopMoving();
        
        
    }

    void OnMove(InputAction.CallbackContext obj)
    {
        _movementDirection = obj.ReadValue<Vector2>().normalized;
        animController.Walk(_movementDirection);
    }

    void StopMoving(InputAction.CallbackContext obj = default)
    {
        _movementDirection = Vector2.zero;
        animController.Idle();
    }
}
