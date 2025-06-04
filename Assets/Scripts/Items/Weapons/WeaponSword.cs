using Player;
using Pool;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerState = Player.PlayerInputController.PlayerState;

namespace Items.Weapons
{
    public class WeaponSword : EquipItems
    {
        [SerializeField] float attackRange;
        [SerializeField] Transform attackRangeCenter;
        [SerializeField] LayerMask enemyLayer;
        [SerializeField] string hitParticleEffectID;

        protected override void OnFireAction(InputAction.CallbackContext obj)
        {
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
                FastPool.Instantiate(hitParticleEffectID, col.transform.position, Quaternion.identity);
            }
        }
    }
}