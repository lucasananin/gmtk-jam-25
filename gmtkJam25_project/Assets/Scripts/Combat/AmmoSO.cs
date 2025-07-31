using UnityEngine;

[CreateAssetMenu(fileName = "Ammo_", menuName = "SO/Combat/Ammo Type")]
public class AmmoSO : ScriptableObject
{
    [SerializeField] string _id = null;
    //[SerializeField] string _displayName = null;
    [SerializeField] int _maxQuantity = 120;

    public string Id { get => _id; private set => _id = value; }
    public int MaxQuantity { get => _maxQuantity; private set => _maxQuantity = value; }
}

[System.Serializable]
public class AmmoModel
{
    [SerializeField] AmmoSO _so = null;
    [SerializeField] int _quantity = 0;

    public int Quantity { get => _quantity; private set => _quantity = value; }

    public void DecreaseQuantity(int _value)
    {
        _quantity -= _value;

        if (_quantity < 0)
            _quantity = 0;
    }

    public void RestoreQuantity()
    {
        _quantity = _so.MaxQuantity;
    }

    public void RestoreQuantity(int _percentage)
    {
        var _value = _so.MaxQuantity * (_percentage / 100f);
        _quantity += Mathf.RoundToInt(_value);
        _quantity = Mathf.Clamp(_quantity, 0, _so.MaxQuantity);
    }

    public bool HasEnoughQuantity(int _value)
    {
        return _quantity >= _value;
    }

    public bool IsFull()
    {
        return _quantity >= _so.MaxQuantity;
    }

    public string GetId()
    {
        return _so.Id;
    }

    public float GetNormalizedValue()
    {
        return _quantity / (_so.MaxQuantity * 1f);
    }
}