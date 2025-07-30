using UnityEngine;

public class PlayerWeaponRotator : WeaponRotator
{
    [SerializeField] InputHandler _input = null;

    private void LateUpdate()
    {
        LookAtMouse();
    }

    public void LookAtMouse()
    {
        if (!CanRotate()) return;

        var _mouseWorldPosition = Camera.main.ScreenToWorldPoint(_input.GetMousePosition());
        LookAtPosition(_mouseWorldPosition);
    }
}
