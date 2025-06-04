using System;
using UI.Inventory;
using UnityEngine;
using PlayerState = PlayerInputController.PlayerState;

namespace Player.Weapons
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] Item_SO itemData;
        [SerializeField] float attackRange;
        [SerializeField] Transform attackRangeCenter;
        [SerializeField] LayerMask enemyLayer;

        Vector2 _attackStartPosition = Vector2.zero;

        public void OnPlayerStateChanged()
        {
            var newState = PlayerInputController.CurrentState;
            if(newState == PlayerState.Attack)
                Attack();
        }

        void Attack()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackRangeCenter.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
               
            }
        }
    }
}