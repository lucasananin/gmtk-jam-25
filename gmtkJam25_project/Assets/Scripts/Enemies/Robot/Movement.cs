using UnityEngine;

public class FloatMotion : MonoBehaviour
{
    [Header("Float Settings")]
    public float floatDistance = 0.5f;  // Distance to float up and down
    public float floatDuration = 1f;    // Time to move up or down

    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer;
    private bool movingUp = true;
    public bool canMove = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * floatDistance;
    }

    void Update()
    {
        if (!canMove) return;
        timer += Time.deltaTime / floatDuration;
        transform.position = Vector3.Lerp(movingUp ? startPos : targetPos, movingUp ? targetPos : startPos, timer);

        if (timer >= 1f)
        {
            timer = 0f;
            movingUp = !movingUp;
        }
    }
}
