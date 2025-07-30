using UnityEngine;

public class PlayerFlipper : SideFlipper
{
    [SerializeField] InputHandler _input = null;

    private void Update()
    {
        FlipToMouse();
    }

    public void FlipToMouse()
    {
        Vector3 _worldMousePosition = Camera.main.ScreenToWorldPoint(_input.GetMousePosition());
        bool _toTheRight = _worldMousePosition.x >= _transform.position.x;
        Flip(_toTheRight);
    }
}
