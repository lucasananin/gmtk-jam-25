using System.Collections;
using Unity.VisualScripting;
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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        StartCoroutine(MeteorSequence());
    }

    IEnumerator MeteorSequence()
    {
        float timer = 0f;

        while (timer < fallDuration)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        if (secondStageSprite != null)
            spriteRenderer.sprite = secondStageSprite;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthBehaviour health = other.GetComponent<HealthBehaviour>();

        if (health != null)
        {
            //EntityBehaviour source = GetComponent<EntityBehaviour>();
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            DamageModel dmg = new(transform, hitPoint, 10);
            health.TakeDamage(dmg);
        }
    }
}
