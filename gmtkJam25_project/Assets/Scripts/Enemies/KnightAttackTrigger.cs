using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private float damage;
    int clamped;

    public void SetDamage(float damage)
    {
        damage = this.damage;
        clamped = Mathf.Clamp(Mathf.RoundToInt(this.damage), 0, 999);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthBehaviour health = other.GetComponent<HealthBehaviour>();
        if (health != null)
        {
            //EntityBehaviour source = GetComponentInParent<EntityBehaviour>();
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            DamageModel dmg = new(transform, hitPoint, clamped);
            health.TakeDamage(dmg);
        }
    }
}
