using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 4;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthBehaviour health = other.GetComponent<HealthBehaviour>();

        if (health != null)
        {
            //EntityBehaviour source = GetComponent<EntityBehaviour>();
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            DamageModel dmg = new(transform, hitPoint, damage);
            health.TakeDamage(dmg);

            Destroy(gameObject);
        }
    }
}
