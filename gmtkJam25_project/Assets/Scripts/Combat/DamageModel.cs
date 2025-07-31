using UnityEngine;

[System.Serializable]
public class DamageModel 
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] Vector3 _pointHit = default;
    [SerializeField] int _value = 0;

    public EntityBehaviour EntitySource { get => _entitySource; private set => _entitySource = value; }
    public Vector3 PointHit { get => _pointHit; private set => _pointHit = value; }
    public int Value { get => _value; private set => _value = value; }

    public DamageModel(EntityBehaviour _entityBehaviour, Vector3 _sourcePosition, int _damageValue)
    {
        _entitySource = _entityBehaviour;
        _pointHit = _sourcePosition;
        _value = _damageValue;
    }
}
