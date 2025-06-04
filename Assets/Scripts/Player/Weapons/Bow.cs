using Pool;
using UnityEngine;
using PlayerState = PlayerInputController.PlayerState;

public class Bow : MonoBehaviour
{
    [SerializeField] string arrowPrefabId;
    [SerializeField] Transform arrowSpawnPoint;
    
    [SerializeField] float chargeTime = 0.5f;
    
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
    
    public void OnPlayerStateChanged()
    {
        var newState = PlayerInputController.CurrentState;
        ToggleCharging(newState == PlayerState.Shooting);
    }

    void ToggleCharging(bool isCharging)
    {
        _isCharging = isCharging;
        
        if(!_isCharging)
            StopCharging();
    }
    
    void StopCharging()
    {
        _isCharging = false;
        _chargeTimer = 0f;
    }
    
    void ShootArrow()
    {
        GameObject arrow = FastPool.Instantiate(arrowPrefabId, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        PlayerInputController.ChangePlayerState(PlayerState.Idle);
    }
}
