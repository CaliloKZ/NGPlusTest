using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    const string WALK_PARAM = "Walking";
    const string SHOOT_PARAM = "Shooting";
    const string HORIZONTAL_PARAM = "Horizontal";
    const string VERTICAL_PARAM = "Vertical";
    
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform bodyTransform;

    [SerializeField] List<Animation> shootAnimations;
    [SerializeField] bool rotateWithScale = true;
    
    Dictionary<AnimatorParameter, int> _parametersHash;

    void Awake()
    {
        if (null == animator) 
            return;
        
        _parametersHash = new Dictionary<AnimatorParameter, int>
        {
            { AnimatorParameter.Walk, Animator.StringToHash(WALK_PARAM) },
            { AnimatorParameter.Shoot, Animator.StringToHash(SHOOT_PARAM) },
            { AnimatorParameter.Horizontal, Animator.StringToHash(HORIZONTAL_PARAM) },
            { AnimatorParameter.Vertical, Animator.StringToHash(VERTICAL_PARAM) }
        };
    }
    
    public void OnPlayerStateChanged()
    {
        var newState = PlayerInputController.CurrentState;
        
        Debug.Log($"PlayerAnimController.OnPlayerStateChanged: {newState}");
        
        ToggleWalk(newState == PlayerInputController.PlayerState.Walking);
        ToggleShooting(newState == PlayerInputController.PlayerState.Shooting);
    }

    void ToggleWalk(bool isWalking)
    {
        if (null == animator) 
            return;
        
        animator.SetBool(_parametersHash[AnimatorParameter.Walk], isWalking);
        if(!isWalking)
            return;
        
        Vector2 movementDirection = PlayerInputController.MovementDirection;
        float horizontalMovement = movementDirection.x;
        
        animator.SetFloat(_parametersHash[AnimatorParameter.Horizontal], horizontalMovement);
        animator.SetFloat(_parametersHash[AnimatorParameter.Vertical], movementDirection.y);
   
        if(!rotateWithScale || horizontalMovement == 0f)
            return;
        
        float horizontalScale = MathF.Sign(horizontalMovement) * Math.Abs(bodyTransform.localScale.x);
        
        bodyTransform.localScale = new Vector3(
            horizontalScale, 
            bodyTransform.localScale.y, 
            bodyTransform.localScale.z);
    }
    
    void ToggleShooting(bool isShooting)
    {
        animator.SetBool(_parametersHash[AnimatorParameter.Shoot], isShooting);
    }
    
    enum AnimatorParameter
    {
        Walk,
        Shoot,
        Horizontal,
        Vertical
    }
}
