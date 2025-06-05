using Player;
using Pool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    public class WeaponBow : EquipItems
    {
        [SerializeField] string arrowPrefabId;
        [SerializeField] Transform arrowSpawnPoint;
    
        [SerializeField] float chargeTime = 0.25f;
    
        float _chargeTimer = 0f;
        bool _isCharging = false;

        void Update()
        {
            if(!_isCharging)
                return;
        
            _chargeTimer += Time.deltaTime;
        
            if (!(_chargeTimer >= chargeTime)) 
                return;
        
            ShootArrow();
            _chargeTimer = 0f;
            _isCharging = false;
        }

        protected override void OnFireAction(InputAction.CallbackContext obj)
        {
            PlayerInputController.ChangePlayerState(PlayerState.Shooting);
        }
    
        public override void OnPlayerStateChanged()
        {
            PlayerState newState = PlayerInputController.CurrentState;
            ToggleCharging(newState == PlayerState.Shooting);
        }

        void ToggleCharging(bool isCharging)
        {
            if (_isCharging && !isCharging)
            {
                StopCharging();
                return;
            }

            _isCharging = isCharging;
        }
    
        void StopCharging()
        {
            _isCharging = false;
            _chargeTimer = 0f;
        }
    
        void ShootArrow()
        {
            FastPool.Instantiate(arrowPrefabId, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            PlayerInputController.ChangePlayerState(PlayerState.Idle);
        }

        public override void OnItemUnequipped()
        {
            base.OnItemUnequipped();
            ToggleCharging(false);
            PlayerInputController.ChangePlayerState(PlayerState.Idle);
        }
    }
}