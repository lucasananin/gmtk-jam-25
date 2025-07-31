using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "SO/Combat/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    [Header("// General")]
    [SerializeField] Sprite _iconSprite = null;
    [SerializeField] WeaponBehaviour _weaponPrefab = null;
    [SerializeField] string _id = null;
    [SerializeField] string _displayName = null;

    [Header("// Holster")]
    [SerializeField] Vector3 _holsterPosition = default;
    [SerializeField] Vector3 _holsterEuler = default;

    [Header("// Projectiles")]
    [SerializeField] ProjectileSO _projectileSO = null;
    //[SerializeField] ProjectileSO _chargedProjectileSO = null;

    [Header("// Stats")]
    [SerializeField] WeaponStats _stats = null;
    [SerializeField] ProjectileStats _projectileStats = null;

    public Sprite SpriteIcon { get => _iconSprite; private set => _iconSprite = value; }
    public WeaponBehaviour WeaponPrefab { get => _weaponPrefab; private set => _weaponPrefab = value; }
    public string Id { get => _id; private set => _id = value; }
    public string DisplayName { get => _displayName; private set => _displayName = value; }
    public Vector3 HolsterPosition { get => _holsterPosition; private set => _holsterPosition = value; }
    public Vector3 HolsterEuler { get => _holsterEuler; private set => _holsterEuler = value; }
    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
    //public ProjectileSO ChargedProjectileSO { get => _chargedProjectileSO; private set => _chargedProjectileSO = value; }

    public WeaponStats Stats { get => _stats; private set => _stats = value; }
    public ProjectileStats ProjectileStats { get => _projectileStats; private set => _projectileStats = value; }

    public bool HasChargeTime()
    {
        return _stats.ChargeTime > 0;
    }

    public bool CanOverheat()
    {
        return _stats.MaxHeat > 0;
    }

    public float GetPullTriggerTotalTime()
    {
        float _offset = 0.1f;
        return _stats.ChargeTime + _offset;
    }

    public float GetTimeUntilAnotherShot()
    {
        float _totalFireRate = _stats.ShotsPerBurst > 0 ? _stats.FireRate * _stats.ShotsPerBurst : _stats.FireRate;
        float _offset = 0.1f;
        return _totalFireRate + _stats.BurstRate + _offset;
    }

    public Quaternion GetProjectileRotation(int _projectileIndex)
    {
        if (_stats.ShootAngle <= 0)
        {
            return Quaternion.identity;
        }

        float _halfAngle = _stats.ShootAngle / 2f;

        if (_stats.ArcMode == ShootArcMode.Spread && _stats.ProjectilesPerShot > 1)
        {
            float _sector = _stats.ShootAngle;
            float _divisionCount = _sector / (_stats.ProjectilesPerShot - 1);
            float _angle = -_halfAngle + _divisionCount * _projectileIndex;
            return Quaternion.AngleAxis(_angle, Vector3.forward);
        }
        else
        {
            float _randomAngle = Random.Range(-_halfAngle, _halfAngle);
            return Quaternion.AngleAxis(_randomAngle, Vector3.forward);
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    [Header("// Damage")]
    [SerializeField, Range(0, 99)] int _damage = 1;
    //[SerializeField, Range(0, 99)] int _chargeShotDamage = 1;
    [SerializeField, Range(0, 99)] int _explosiveDamage = 1;

    [Header("// Fire Rate")]
    [SerializeField, Range(0.01f, 9f)] float _fireRate = 0.1f;

    [Header("// Charge")]
    [SerializeField, Range(0f, 9f)] float _chargeTime = 0f;

    [Header("// Heat")]
    [SerializeField, Range(0f, 99f)] float _maxHeat = 0f;
    [SerializeField, Range(0f, 99f)] float _heatPerShot = 0f;
    [SerializeField, Range(0f, 99f)] float _heatDecreasePerSecond = 0f;
    [SerializeField, Range(0f, 99f)] float _maxOverheatTime = 0f;

    [Header("// Precision")]
    [SerializeField, Range(0, 360)] float _shootAngle = 0;
    [SerializeReference] ShootArcMode _arcMode = default;

    [Header("// Ammo")]
    [SerializeField, Range(1, 36)] int _projectilesPerShot = 1;
    [SerializeField, Range(0, 99)] int _ammoPerShot = 1;

    [Header("- WEAPON SPECIFICS -")]

    [Header("// Semi-Auto Behaviour: Properties")]
    [SerializeField] bool _isAutoChargeType = false;
    //[SerializeField] bool _hasChargeWeakShot = false;

    [Header("// Burst Behaviour: Properties")]
    [SerializeField, Range(0f, 9f)] float _burstRate = 0f;
    [SerializeField, Range(0, 99)] int _shotsPerBurst = 0;

    public int Damage { get => _damage; private set => _damage = value; }
    //public int ChargeShotDamage { get => _chargeShotDamage; private set => _chargeShotDamage = value; }
    public int ExplosiveDamage { get => _explosiveDamage; private set => _explosiveDamage = value; }

    public float FireRate { get => _fireRate; private set => _fireRate = value; }

    public float ChargeTime { get => _chargeTime; set => _chargeTime = value; }

    public float MaxHeat { get => _maxHeat; private set => _maxHeat = value; }
    public float HeatPerShot { get => _heatPerShot; private set => _heatPerShot = value; }
    public float HeatDecreasePerSecond { get => _heatDecreasePerSecond; private set => _heatDecreasePerSecond = value; }
    public float MaxOverheatTime { get => _maxOverheatTime; private set => _maxOverheatTime = value; }

    public float ShootAngle { get => _shootAngle; set => _shootAngle = value; }
    public ShootArcMode ArcMode { get => _arcMode; set => _arcMode = value; }

    public int ProjectilesPerShot { get => _projectilesPerShot; private set => _projectilesPerShot = value; }
    public int AmmoPerShot { get => _ammoPerShot; set => _ammoPerShot = value; }

    public bool IsAutoChargeType { get => _isAutoChargeType; private set => _isAutoChargeType = value; }
    //public bool HasChargeWeakShot { get => _hasChargeWeakShot; private set => _hasChargeWeakShot = value; }

    public float BurstRate { get => _burstRate; private set => _burstRate = value; }
    public int ShotsPerBurst { get => _shotsPerBurst; set => _shotsPerBurst = value; }
}

public enum ShootArcMode
{
    Random,
    Spread,
}