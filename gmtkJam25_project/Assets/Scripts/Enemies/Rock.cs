using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 2f;
    public int damage;
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Launch(Vector3 dir, Sprite rockSprite)
    {
        direction = dir.normalized;
        if (spriteRenderer != null)
            spriteRenderer.sprite = rockSprite;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthBehaviour health = other.GetComponent<HealthBehaviour>();

        if (health != null)
        {
            EntityBehaviour source = GetComponent<EntityBehaviour>();
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            DamageModel dmg = new DamageModel(source, hitPoint, damage);
            health.TakeDamage(dmg);

            Destroy(gameObject);
        }
    }

}
