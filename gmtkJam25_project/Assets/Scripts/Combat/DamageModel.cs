using UnityEngine;

[System.Serializable]
public class DamageModel
{
    [SerializeField] Transform _entitySource = null;
    [SerializeField] Vector3 _pointHit = default;
    [SerializeField] int _value = 0;
    [SerializeField] float _knockbackSpeed = 20f;
    [SerializeField] float _knockbackDuration = 0.3f;

    //public EntityBehaviour EntitySource { get => _entitySource; private set => _entitySource = value; }
    public Vector3 PointHit { get => _pointHit; private set => _pointHit = value; }
    public int Value { get => _value; private set => _value = value; }
    public float KnockbackDuration { get => _knockbackDuration; set => _knockbackDuration = value; }

    public DamageModel(Transform _entityBehaviour, Vector3 _sourcePosition, int _damageValue)
    {
        _entitySource = _entityBehaviour;
        _pointHit = _sourcePosition;
        _value = _damageValue;
    }

    public Vector2 GetKnockbackVelocity(Transform _target)
    {
        return (_target.position - _entitySource.transform.position).normalized * _knockbackSpeed;
    }
}
