using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float stayDuration = 2f;
    public float yOffset = 2f;
    public bool Laser { get; private set; }

    private Vector3 originalPosition;
    private Coroutine moveRoutine;
    private FloatMotion floatMotion;

    void Start()
    {
        originalPosition = transform.position;
        floatMotion = GetComponent<FloatMotion>();
    }

    public void ActivateLaserTowards(Transform player)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(TrackPlayerAndReturn(player));
    }

    IEnumerator TrackPlayerAndReturn(Transform player)
    {
        if (floatMotion != null)
            floatMotion.enabled = false;

        Laser = true;

        float elapsed = 0f;

        // Follow player for duration
        while (elapsed < stayDuration)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Laser = false;

        // Return to original position
        while (Vector3.Distance(transform.position, originalPosition) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

        if (floatMotion != null)
            floatMotion.enabled = true;

        moveRoutine = null;
    }
}
