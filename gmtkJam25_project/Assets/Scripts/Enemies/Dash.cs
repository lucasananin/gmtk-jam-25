using UnityEngine;

public class DashFollower : MonoBehaviour
{
    public Bear bear;
    public float moveSpeed = 10f;

    private void Start()
    {
        bear = GetComponentInParent<Bear>();
    }

    void Update()
    {
        if (bear == null) return;

        Vector3 dashDir = bear.GetDashDirection();
        if (dashDir != Vector3.zero)
        {
            transform.position += dashDir.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
