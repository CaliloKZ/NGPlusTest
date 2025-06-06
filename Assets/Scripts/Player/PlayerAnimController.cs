using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimController : MonoBehaviour
    {
        const string WALK_PARAM = "Walking";
        const string SHOOT_PARAM = "Shooting";
        const string ATTACK_PARAM = "Attack";
        const string HORIZONTAL_PARAM = "Horizontal";
        const string VERTICAL_PARAM = "Vertical";
    
        [SerializeField] Animator animator;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Transform bodyTransform;
        [SerializeField] Transform meleeAttackRangeCenter;
        
        [SerializeField] List<Sprite> playerSprites = new List<Sprite>();
    
        Dictionary<AnimatorParameter, int> _parametersHash;
        Dictionary<string, Sprite> _animationSprites;

        void Awake()
        {
            if (null == animator) 
                return;
        
            _parametersHash = new Dictionary<AnimatorParameter, int>
            {
                { AnimatorParameter.Walk, Animator.StringToHash(WALK_PARAM) },
                { AnimatorParameter.Shoot, Animator.StringToHash(SHOOT_PARAM) },
                { AnimatorParameter.Attack, Animator.StringToHash(ATTACK_PARAM) },
                { AnimatorParameter.Horizontal, Animator.StringToHash(HORIZONTAL_PARAM) },
                { AnimatorParameter.Vertical, Animator.StringToHash(VERTICAL_PARAM) }
            };
            
            _animationSprites = new Dictionary<string, Sprite>
            {
                { "IdleUp", playerSprites[0]},
                { "IdleDown",  playerSprites[1]},
                { "IdleRight",  playerSprites[2]},
                { "IdleLeft", playerSprites[3]},
                { "WalkUp", playerSprites[0]},
                { "WalkDown", playerSprites[1]},
                { "WalkRight", playerSprites[2]},
                { "WalkLeft", playerSprites[3]},
                { "ShootUp", playerSprites[4]},
                { "ShootDown", playerSprites[5]},
                { "ShootRight", playerSprites[6]},
                { "ShootLeft", playerSprites[7]},
                { "AttackUp", playerSprites[4]},
                { "AttackDown", playerSprites[5]},
                { "AttackRight", playerSprites[6]},
                { "AttackLeft", playerSprites[7]}
            };
        }

        private void FixedUpdate()
        {
            string animationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            spriteRenderer.sprite = _animationSprites[animationName];
        }
    
        public void OnPlayerStateChanged()
        {
            var newState = PlayerInputController.CurrentState;
        
            ToggleWalk(newState == PlayerState.Walking);
            ToggleShooting(newState == PlayerState.Shooting);
        
            if (newState == PlayerState.Attack)
                Attack();
        }

        void ToggleWalk(bool isWalking)
        {
            if (null == animator) 
                return;
        
            animator.SetBool(_parametersHash[AnimatorParameter.Walk], isWalking);
            if(!isWalking)
                return;
        
            Vector2 movementDirection = PlayerInputController.MovementDirection;
        
            animator.SetFloat(_parametersHash[AnimatorParameter.Horizontal], movementDirection.x);
            animator.SetFloat(_parametersHash[AnimatorParameter.Vertical], movementDirection.y);
        }
    
        void Attack()
        {
            animator.SetTrigger(_parametersHash[AnimatorParameter.Attack]);
        }
    
        void ToggleShooting(bool isShooting)
        {
            animator.SetBool(_parametersHash[AnimatorParameter.Shoot], isShooting);
        }
    
        enum AnimatorParameter
        {
            Walk,
            Shoot,
            Attack,
            Horizontal,
            Vertical
        }
    }
}
