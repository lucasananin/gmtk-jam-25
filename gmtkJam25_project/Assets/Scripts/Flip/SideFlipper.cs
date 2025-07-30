using UnityEngine;

public class SideFlipper : MonoBehaviour
{
    [SerializeField] protected Transform _transform = null;

    public void Flip(bool _toTheRight)
    {
        var _newScale = Vector2.one;

        if (!_toTheRight)
            _newScale.x = -1f;

        _transform.localScale = _newScale;
    }
}
