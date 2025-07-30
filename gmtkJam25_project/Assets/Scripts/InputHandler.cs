using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput = null;

    public Vector2 Move { get => _moveInput.action.ReadValue<Vector2>(); }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}
