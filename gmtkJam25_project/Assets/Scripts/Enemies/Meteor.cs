using System.Collections;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Movement Settings")]
    public float fallDuration = 2f;
    public float fallSpeed = 3f;

    [Header("Visuals")]
    public Sprite secondStageSprite;
    public float secondStageDuration = 1f;
    public GameObject finalVisualSource;
    public GameObject redcircle;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private bool hasCollided = false;
    private Coroutine meteorCoroutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        meteorCoroutine = StartCoroutine(MeteorSequence());
    }

    IEnumerator MeteorSequence()
    {
        float timer = 0f;

        while (timer < fallDuration && !hasCollided)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(DoImpactVisuals());
    }

    IEnumerator DoImpactVisuals()
    {
        if (secondStageSprite != null)
            spriteRenderer.sprite = secondStageSprite;

        if (redcircle != null)
            Destroy(redcircle);

        yield return new WaitForSeconds(secondStageDuration);

        if (finalVisualSource != null)
        {
            SpriteRenderer sourceRenderer = finalVisualSource.GetComponent<SpriteRenderer>();
            if (sourceRenderer != null)
                spriteRenderer.sprite = sourceRenderer.sprite;

            float randomZ = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0f, 0f, randomZ);
        }

        if (col != null)
            col.enabled = false;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided) return;

        HealthBehaviour health = other.GetComponent<HealthBehaviour>();
        if (health != null)
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            DamageModel dmg = new(transform, hitPoint, 10);
            health.TakeDamage(dmg);
        }

        hasCollided = true;
        if (meteorCoroutine != null)
            StopCoroutine(meteorCoroutine);

        StartCoroutine(DoImpactVisuals());
    }
}
