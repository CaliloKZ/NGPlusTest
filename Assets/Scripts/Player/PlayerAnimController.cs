using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    const string WALK_TRIGGER = "Walk";
    const string STOP_TRIGGER = "Stop";
    const string DIRECTION_PARAM = "Direction";
    
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    Dictionary<AnimatorParameter, int> _parametersHash;

    void Awake()
    {
        if (null == animator) 
            return;

        _parametersHash = new Dictionary<AnimatorParameter, int>();

        _parametersHash.Add(AnimatorParameter.WALK, Animator.StringToHash(WALK_TRIGGER));
        _parametersHash.Add(AnimatorParameter.STOP, Animator.StringToHash(STOP_TRIGGER));
        _parametersHash.Add(AnimatorParameter.DIRECTION, Animator.StringToHash(DIRECTION_PARAM));
    }
    
    public void Walk(Vector2 movementDirection)
    {
        if (null == animator) 
            return;
        
        animator.SetTrigger(_parametersHash[AnimatorParameter.WALK]);
        animator.SetInteger(_parametersHash[AnimatorParameter.DIRECTION], Convert.ToInt32(movementDirection.y));
        
        float horizontalScale = movementDirection.x < 0? -Math.Abs(transform.localScale.x) : Math.Abs(transform.localScale.x);
        
        transform.localScale = new Vector3(
            horizontalScale, 
            transform.localScale.y, 
            transform.localScale.z);
    }

    public void Idle()
    {
       animator.SetTrigger(_parametersHash[AnimatorParameter.STOP]);
    }
    
    enum AnimatorParameter
    {
        WALK,
        STOP,
        DIRECTION  
    }
}
