using UnityEngine;

public class BurstWeapon : WeaponBehaviour
{
    [Header("// READONLY")]
    [SerializeField] float _nextBurst = 0;
    [SerializeField] int _currentShootCount = 0;
    [SerializeField] bool _isBursting = false;

    protected override void Awake()
    {
        base.Awake();
        _nextBurst = _weaponSO.Stats.BurstRate;
    }

    private void FixedUpdate()
    {
        if (_isBursting)
        {
            _nextFire += _nextFire < _weaponSO.Stats.FireRate ? Time.fixedDeltaTime : 0;

            if (_nextFire >= _weaponSO.Stats.FireRate)
            {
                Shoot();

                _currentShootCount++;

                if (_currentShootCount >= _weaponSO.Stats.ShotsPerBurst)
                {
                    _nextFire = _weaponSO.Stats.FireRate;
                    _isBursting = false;
                }
            }
        }
        else
        {
            _nextBurst += _nextBurst < _weaponSO.Stats.BurstRate? Time.fixedDeltaTime : 0;
        }
    }

    public override void PullTrigger()
    {
        if (!HasAmmo()) return;
        if (_isOverheated) return;
        if (_nextBurst < _weaponSO.Stats.BurstRate) return;

        _nextBurst -= _weaponSO.Stats.BurstRate;
        _currentShootCount = 0;
        _isBursting = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        base.ReleaseTrigger();
    }
}
