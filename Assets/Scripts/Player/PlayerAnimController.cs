using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    const string WALK_TRIGGER = "Walk";
    const string STOP_TRIGGER = "Stop";
    const string SHOOT_TRIGGER = "Shoot";
    const string DIRECTION_PARAM = "Direction";
    
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    Dictionary<AnimatorParameter, int> _parametersHash;

    void Awake()
    {
        if (null == animator) 
            return;
        
        PlayerInputController.OnMovement += Walk;
        PlayerInputController.OnPlayerShoot += Shoot;
        PlayerInputController.OnStop += Idle;
        
        _parametersHash = new Dictionary<AnimatorParameter, int>();

        _parametersHash.Add(AnimatorParameter.Walk, Animator.StringToHash(WALK_TRIGGER));
        _parametersHash.Add(AnimatorParameter.Stop, Animator.StringToHash(STOP_TRIGGER));
        _parametersHash.Add(AnimatorParameter.Shoot, Animator.StringToHash(SHOOT_TRIGGER));
        _parametersHash.Add(AnimatorParameter.Direction, Animator.StringToHash(DIRECTION_PARAM));
    }

    void OnDestroy()
    {
        PlayerInputController.OnMovement -= Walk;
        PlayerInputController.OnPlayerShoot -= Shoot;
        PlayerInputController.OnStop -= Idle;
    }

    public void Walk(Vector2 movementDirection)
    {
        if (null == animator) 
            return;
        
        animator.SetTrigger(_parametersHash[AnimatorParameter.Walk]);
        animator.SetInteger(_parametersHash[AnimatorParameter.Direction], Convert.ToInt32(movementDirection.y));
        
        float horizontalScale = movementDirection.x < 0? -Math.Abs(transform.localScale.x) : Math.Abs(transform.localScale.x);
        
        transform.localScale = new Vector3(
            horizontalScale, 
            transform.localScale.y, 
            transform.localScale.z);
    }

    public void Idle()
    {
       animator.SetTrigger(_parametersHash[AnimatorParameter.Stop]);
    }

    public void Shoot()
    {
        animator.SetTrigger(_parametersHash[AnimatorParameter.Shoot]);
    }
    
    enum AnimatorParameter
    {
        Walk,
        Stop,
        Shoot,
        Direction  
    }
}
