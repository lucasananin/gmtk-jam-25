using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [SerializeField] bool _infiniteAmmo = false;
    [SerializeField, Range(0f, 99f)] int _initialAmmoPercentage = 50;
    [SerializeField] AmmoModel[] _models = null;

    public AmmoModel[] Models { get => _models; private set => _models = value; }

    public event System.Action OnAmmoChanged = null;

    private void Awake()
    {
        RestoreAmmo(_initialAmmoPercentage);
    }

    public void RestoreFullAmmo()
    {
        RestoreAmmo(999);
    }

    public void RestoreAmmo(int _percentage)
    {
        int _count = _models.Length;

        for (int i = 0; i < _count; i++)
        {
            _models[i].RestoreQuantity(_percentage);
        }

        OnAmmoChanged?.Invoke();
    }

    public void RestoreOneAmmoModel(int _percentage)
    {
        var _weaponHandler = GetComponent<PlayerWeaponHandler>();

        var _ammoTypes = _weaponHandler.GetAmmoTypes();
        var _filteredList = FilterAmmoTypes(_ammoTypes);
        var _randomIndex = Random.Range(0, _filteredList.Count);

        var _ammoSO = _filteredList[_randomIndex];
        GetModel(_ammoSO).RestoreQuantity(_percentage);
        OnAmmoChanged?.Invoke();
        //Debug.Log($"// {_ammoSO.Id} ammo restored.");
    }

    private List<AmmoSO> FilterAmmoTypes(List<AmmoSO> _list)
    {
        var _newList = new List<AmmoSO>(_list);
        int _count = _newList.Count;

        for (int i = _count - 1; i >= 0; i--)
        {
            var _so = _newList[i];

            if (GetModel(_so).IsFull())
            {
                _newList.RemoveAt(i);
            }
        }

        return _newList.Count > 0 ? _newList : _list;
    }

    public void DecreaseAmmo(ProjectileSO _projectileSO, WeaponSO _weaponSO)
    {
        if (_infiniteAmmo) return;

        var _model = GetModel(_projectileSO.AmmoSO);
        _model?.DecreaseQuantity(_weaponSO.Stats.AmmoPerShot);
        OnAmmoChanged?.Invoke();
    }

    public bool HasAmmo(ProjectileSO _projectileSO, WeaponSO _weaponSO)
    {
        var _model = GetModel(_projectileSO.AmmoSO);
        return _model is not null && _model.HasEnoughQuantity(_weaponSO.Stats.AmmoPerShot);
    }

    public int GetAmmoQuantity(ProjectileSO _projectileSO)
    {
        var _model = GetModel(_projectileSO.AmmoSO);
        return _model is not null ? _model.Quantity : -1;
    }

    private AmmoModel GetModel(AmmoSO _ammoSO)
    {
        int _count = _models.Length;

        for (int i = 0; i < _count; i++)
        {
            var _model = _models[i];
            var _isTheSameId = GeneralMethods.IsTheSameString(_ammoSO.Id, _model.GetId());

            if (_isTheSameId)
            {
                return _model;
            }
        }

        return null;
    }
}
