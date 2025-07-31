using UnityEngine;

public class AutoWeapon : WeaponBehaviour
{
    [Header("// READONLY")]
    [SerializeField] bool _isHoldingTrigger = false;

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _weaponSO.Stats.FireRate ? Time.fixedDeltaTime : 0;

        UpdateChargeTimer();

        if (!HasEnoughChargeTimer()) return;
        if (!HasAmmo()) return;

        if (_nextFire >= _weaponSO.Stats.FireRate && _isHoldingTrigger && !_isOverheated)
        {
            Shoot();
        }
    }

    public override void PullTrigger()
    {
        _isCharging = true;
        _isHoldingTrigger = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        _isCharging = false;
        _isHoldingTrigger = false;
        base.ReleaseTrigger();
    }
}
