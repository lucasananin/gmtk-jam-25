using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] InputHandler _input = null;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _dodgeSpeed = 5f;
    [SerializeField] float _dodgeDuration = 1f;

    [Header("// READONLY")]
    [SerializeField] float _dodgeTime = 0f;

    //private void FixedUpdate()
    //{
    //    MoveWithInput();
    //}

    public void MoveWithInput()
    {
        Vector2 _velocity = _input.Move * _moveSpeed;
        _rb.linearVelocity = _velocity;
    }

    public bool TryStartDodge()
    {
        if (_input.Move == Vector2.zero) return false;
        if (!_input.Dodge) return false;

        _dodgeTime = 0;
        Vector2 _velocity = _input.Move * _dodgeSpeed;
        _rb.linearVelocity = _velocity;
        return true;
    }

    public void IncreaseDodgeTime()
    {
        _dodgeTime += Time.deltaTime;
    }

    public bool IsDodging()
    {
        return _dodgeTime < _dodgeDuration;
    }
}
