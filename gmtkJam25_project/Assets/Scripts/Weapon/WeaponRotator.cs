using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] bool _canRotate = true;

    public void LookAtPosition(Vector3 _position)
    {
        if (!CanRotate()) return;

        var _direction = transform.parent.InverseTransformPoint(_position);
        var _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, _angle);
    }

    public void ResetRotation()
    {
        var _parentForwardPosition = transform.position + new Vector3(transform.parent.right.x, 0, 0);
        var _direction = transform.parent.InverseTransformPoint(_parentForwardPosition);
        var _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, _angle);
    }

    public bool CanRotate()
    {
        if (!_canRotate)
            ResetRotation();

        return _canRotate;
    }
}
