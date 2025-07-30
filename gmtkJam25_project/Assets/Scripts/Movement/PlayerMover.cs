using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] InputHandler _input = null;
    [SerializeField] float _moveSpeed = 5f;

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 _velocity = _input.Move * _moveSpeed;
        _rb.linearVelocity = _velocity;
    }
}
