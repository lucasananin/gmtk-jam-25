using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entitySource = null;
    //[SerializeField] SideFlipper _entityFlipper = null;
    [SerializeField] AmmoHandler _ammoHandler = null;
    //[SerializeField] EntityHolster _holster = null;
    [SerializeField] InputHandler _input = null;
    [SerializeField, Range(1, 9)] int _maxWeaponsCount = 2;
    [SerializeField, Range(0f, 1f)] float _swapInputDelay = 0.3f;
    [SerializeField] List<WeaponBehaviour> _weaponsList = null;

    [Header("// READONLY")]
    [SerializeField] WeaponBehaviour _currentWeapon = null;
    [SerializeField] WeaponBehaviour _lastWeapon = null;
    //[SerializeField] WeaponRotator _weaponRotator = null;
    [SerializeField] int _currentWeaponIndex = 0;
    [SerializeField] int _lastWeaponIndex = 0;
    [SerializeField] bool _isWaitingSwapDelay = false;
    [SerializeField] bool _canRotateWeapon = false;

    public event System.Action OnWeaponAdded = null;
    public event System.Action OnWeaponSwapped = null;

    public bool CanRotateWeapon { get => _canRotateWeapon; set => _canRotateWeapon = value; }

    private void Awake()
    {
        SetCurrentWeapon();
    }

    //private void OnEnable()
    //{
    //    _entityFlipper.OnFlip += RotateCurrentWeapon;
    //}

    //private void OnDisable()
    //{
    //    _entityFlipper.OnFlip -= RotateCurrentWeapon;
    //}

    private void Update()
    {
        if (_input.PullTrigger)
        {
            PullTrigger();
        }
         
        if (_input.ReleaseTrigger)
        {
            ReleaseTrigger();
        }
    }

    private void FixedUpdate()
    {
        if (_canRotateWeapon)
        {
            RotateCurrentWeapon();
        }
    }

    public void PullTrigger()
    {
        if (_currentWeapon)
            _currentWeapon.PullTrigger();
    }

    public void ReleaseTrigger()
    {
        if (_currentWeapon)
            _currentWeapon.ReleaseTrigger();
    }

    public void SwapThroughInput(float _y)
    {
        if (_isWaitingSwapDelay) return;
        if (_weaponsList.Count < 2) return;

        if (_y > 0)
            SwapToNextWeapon();
        else if (_y < 0)
            SwapToPreviousWeapon();

        OnWeaponSwapped?.Invoke();
        StartCoroutine(DisableWeaponSwap_routine());
    }

    private void SwapToNextWeapon()
    {
        IncreaseWeaponIndex(1);
    }

    private void SwapToPreviousWeapon()
    {
        IncreaseWeaponIndex(-1);
    }

    private void IncreaseWeaponIndex(int _value)
    {
        _lastWeaponIndex = HasMoreThanOneWeapon() ? _currentWeaponIndex : -1;
        _currentWeaponIndex = _weaponsList.IndexOf(_currentWeapon);
        _currentWeaponIndex += _value;

        if (_currentWeaponIndex >= _weaponsList.Count)
            _currentWeaponIndex = 0;

        if (_currentWeaponIndex < 0)
            _currentWeaponIndex = _weaponsList.Count - 1;

        SetCurrentWeapon();
    }

    private void SetCurrentWeapon()
    {
        if (_weaponsList.Count <= 0) return;

        _currentWeapon = _weaponsList[_currentWeaponIndex];
        _lastWeapon = HasMoreThanOneWeapon() ? _weaponsList[_lastWeaponIndex] : null;
        _currentWeapon.Init(_entitySource, _ammoHandler);

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        int _count = _weaponsList.Count;

        for (int i = 0; i < _count; i++)
        {
            bool _isCurrentWeapon = i == _currentWeaponIndex;
            _weaponsList[i].gameObject.SetActive(_isCurrentWeapon);
        }

        //_weaponRotator = _currentWeapon.GetComponent<WeaponRotator>();
        RotateCurrentWeapon();
        UpdateHolster();
    }

    private void UpdateHolster()
    {
        //bool _canUpdate = _lastWeapon is not null && HasMoreThanOneWeapon();
        //var _so = _canUpdate ? _lastWeapon.WeaponSO : null;
        //_holster.Init(_so);
    }

    public void RotateCurrentWeapon()
    {
        //_weaponRotator?.LookAtMouse();
    }

    public void AddWeapon(WeaponSO _weaponSO, out WeaponSO _droppedWeaponSO)
    {
        var _newWeapon = Instantiate(_weaponSO.WeaponPrefab, transform.position, Quaternion.identity, transform);

        if (_weaponsList.Count >= _maxWeaponsCount)
        {
            _weaponsList[_currentWeaponIndex] = _newWeapon;
            _droppedWeaponSO = _currentWeapon.WeaponSO;
            Destroy(_currentWeapon.gameObject);
            SetCurrentWeapon();
        }
        else
        {
            _weaponsList.Add(_newWeapon);
            _droppedWeaponSO = null;
            SwapToNextWeapon();
        }

        OnWeaponAdded?.Invoke();
    }

    public bool HasWeapon(WeaponSO _weaponSO)
    {
        int _count = _weaponsList.Count;

        for (int i = 0; i < _count; i++)
        {
            if (_weaponsList[i].GetId() == _weaponSO.Id)
            {
                return true;
            }
        }

        return false;
    }

    private bool HasMoreThanOneWeapon()
    {
        return _weaponsList.Count > 1;
    }

    public WeaponBehaviour GetFirstWeapon()
    {
        if (_currentWeapon is null) return null;
        return _weaponsList[0];
    }

    public WeaponBehaviour GetSecondWeapon()
    {
        if (!HasMoreThanOneWeapon()) return null;
        return _weaponsList[1];
    }

    private IEnumerator DisableWeaponSwap_routine()
    {
        _isWaitingSwapDelay = true;
        yield return new WaitForSeconds(_swapInputDelay);
        _isWaitingSwapDelay = false;
    }

    public List<AmmoSO> GetAmmoTypes()
    {
        var _list = new List<AmmoSO>();
        var _count = _weaponsList.Count;

        for (int i = 0; i < _count; i++)
        {
            var _ammoSO = _weaponsList[i].WeaponSO.ProjectileSO.AmmoSO;
            _list.Add(_ammoSO);
        }

        return _list;
    }
}
