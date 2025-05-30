using System;
using Pool;
using UnityEngine;

public class HeroShooting : MonoBehaviour
{
    [SerializeField] string arrowPrefabId;
    [SerializeField] Transform arrowSpawnPoint;
    
    [SerializeField] float chargeTime = 2f;
    
    float _chargeTimer = 0f;
    bool _isCharging = false;

    void Start()
    {
        PlayerInputController.OnPlayerShoot += StartCharging;
        PlayerInputController.OnMovement += OnMove;
        PlayerInputController.OnStop += StopCharging;
    }

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

    void StartCharging()
    {
        _isCharging = true;
        Debug.Log($"onShoot2");
        PlayerInputController.OnPlayerShoot -= StartCharging;
    }

    void OnMove(Vector2 movementDirection)
    {
        StopCharging();
    }
    
    void StopCharging()
    {
        _isCharging = false;
        _chargeTimer = 0f;
        PlayerInputController.OnPlayerShoot += StartCharging;
    }
    
    void ShootArrow()
    {
        GameObject arrow = FastPool.Instantiate(arrowPrefabId, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        PlayerInputController.OnPlayerShoot += StartCharging;
    }
}
