using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput = null;
    [SerializeField] InputActionReference _shootInput = null;
    [SerializeField] InputActionReference _dodgeInput = null;
    [SerializeField] InputActionReference _swapWeaponInput = null;

    public Vector2 Move { get => _moveInput.action.ReadValue<Vector2>(); }
    public bool PullTrigger { get => _shootInput.action.WasPressedThisFrame(); }
    public bool ReleaseTrigger { get => _shootInput.action.WasReleasedThisFrame(); }
    public bool Dodge { get => _dodgeInput.action.WasPerformedThisFrame(); }
    public Vector2 SwapDelta { get => _swapWeaponInput.action.ReadValue<Vector2>(); }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}
