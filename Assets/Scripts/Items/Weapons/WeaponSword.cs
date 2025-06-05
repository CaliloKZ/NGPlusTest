using Player;
using Pool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items.Weapons
{
    public class WeaponSword : EquipItems
    {
        [SerializeField] float attackRange;
        [SerializeField] float cooldown = 0.5f;
        [SerializeField] Transform attackRangeCenter;
        [SerializeField] LayerMask enemyLayer;
        [SerializeField] string hitParticleEffectID;

        bool _canAttack = false;
        float _cooldownTimer = 0f;
        
        void Update()
        {
            if (_canAttack)
                return;

            _cooldownTimer += Time.deltaTime;
            if (!(_cooldownTimer >= cooldown)) 
                return;
            
            _canAttack = true;
            _cooldownTimer = 0f;
        }

        protected override void OnFireAction(InputAction.CallbackContext obj)
        {
            if(!_canAttack)
                return;
            
            _canAttack = false;
            PlayerInputController.ChangePlayerState(PlayerState.Attack);
        }

        public override void OnPlayerStateChanged()
        {
            var newState = PlayerInputController.CurrentState;
            if(newState == PlayerState.Attack)
                Attack();
        }

        void Attack()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackRangeCenter.position, attackRange, enemyLayer);
            foreach (Collider2D col in hitColliders)
            {
                FastPool.Instantiate(hitParticleEffectID, attackRangeCenter.position, Quaternion.identity);
            }
            
        }
    }
}