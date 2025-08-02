using UnityEngine;

public class PlayerEntity : EntityBehaviour
{
    [SerializeField] InputHandler _input = null;
    [SerializeField] PlayerMover _mover = null;
    [SerializeField] PlayerWeaponHandler _weaponHandler = null;

    [Header("// READONLY")]
    [SerializeField] PlayerState _state = default;

    private void Update()
    {
        switch (_state)
        {
            case PlayerState.move:
                _weaponHandler.CheckTriggerInput();
                if (_mover.TryStartDodge())
                {
                    _state = PlayerState.dodge;
                    _healthBehaviour.SetInvincibility(true);
                }
                break;
            case PlayerState.dodge:
                _mover.IncreaseDodgeTime();
                if (!_mover.IsDodging())
                {
                    _state = PlayerState.move;
                    _healthBehaviour.SetInvincibility(false);
                }
                break;
            case PlayerState.knockback:
                _mover.IncreaseKnockbackTime();
                if (!_mover.IsKnockbacking())
                {
                    _state = PlayerState.move;
                }
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case PlayerState.move:
                _mover.MoveWithInput();
                break;
            case PlayerState.dodge:
                break;
            default:
                break;
        }
    }

    internal void StartKnockback()
    {
        _state = PlayerState.knockback;
        var _velocity = _healthBehaviour.LastDamageModel.GetKnockbackVelocity(transform);
        _mover.Knockback(_velocity, _healthBehaviour.LastDamageModel.KnockbackDuration);
    }

    //private void OnValidate()
    //{
    //    if (_mover == null)
    //        _mover = GetComponent<PlayerMover>();
    //}

    //private void OnEnable()
    //{
    //    EnemySpawner.OnEndWaveGroupChanged += ResetPosition;
    //}

    //private void OnDisable()
    //{
    //    EnemySpawner.OnEndWaveGroupChanged -= ResetPosition;
    //}

    public override bool IsMoving()
    {
        return _input.Move != Vector2.zero;
    }

    //private void ResetPosition(WaveSO _waveSo)
    //{
    //    transform.position = Vector3.zero;
    //}
}

public enum PlayerState
{
    move,
    dodge,
    knockback,
}