using System.Collections;
using UnityEngine;

public abstract class HealthBehaviour : MonoBehaviour
{
    [SerializeField] protected bool _isInvincible = false;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected float _deathDelay = 0f;

    [Header("// READONLY")]
    [SerializeField] protected int _currentHealth = 0;
    [SerializeField] protected bool _isDying = false;
    [SerializeField] protected DamageModel _lastDamageModel = null;

    public DamageModel LastDamageModel { get => _lastDamageModel; private set => _lastDamageModel = value; }

    public event System.Action OnDamageTaken = null;
    public event System.Action OnDead = null;
    public event System.Action OnRestored = null;

    private void Awake()
    {
        RestoreHealth();
    }

    public void TakeDamage()
    {
        var _damageModel = new DamageModel(null, transform.position, 1);
        TakeDamage(_damageModel);
    }

    public void ForceDie()
    {
        var _damageModel = new DamageModel(null, transform.position, _currentHealth);
        TakeDamage(_damageModel);
    }

    public void TakeDamage(DamageModel _damageModel)
    {
        _lastDamageModel = _damageModel;
        _currentHealth -= _damageModel.Value;

        if (_isInvincible)
            RestoreHealth();

        if (_currentHealth <= 0 && !_isDying)
        {
            Die();
        }
        else
        {
            OnDamageTaken_();
        }
    }

    public void RestoreHealth()
    {
        _isDying = false;
        RestoreHealth(999);
    }

    public void RestoreHealth(int _percentage)
    {
        var _value = _maxHealth * (_percentage / 100f);
        _currentHealth += Mathf.RoundToInt(_value);
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnRestored?.Invoke();
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    public virtual float GetNormalizedValue()
    {
        return _currentHealth / (_maxHealth * 1f);
    }

    protected virtual void OnDead_()
    {
        _isDying = true;
        _currentHealth = 0;
        OnDead?.Invoke();
    }

    protected virtual void OnDamageTaken_()
    {
        OnDamageTaken?.Invoke();
    }

    private void Die()
    {
        if (_deathDelay > 0)
        {
            StartCoroutine(Dead_routine());
        }
        else
        {
            OnDead_();
        }
    }

    private IEnumerator Dead_routine()
    {
        yield return new WaitForSeconds(_deathDelay);
        OnDead_();
    }
}
