using UnityEngine;
using System.Collections;

public class Knight : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject dashAttackTrigger;
    public GameObject closeAttackTrigger;

    [Header("Ranges")]
    public float closeRange = 2f;
    public float mediumRange = 5f;
    public float longRange = 10f;
    public float chaseSpeed = 3f;
    public float minChaseDistance = 1.5f;

    [Header("Attack Settings")]
    public float closeAttackCharge = 0.3f;
    public float mediumAttackCharge = 0.6f;
    public float smashAttackCharge = 1f;
    public int closeAttackDamage = 5;
    public int mediumAttackDamage = 10;
    public int smashAttackDamage = 20;

    [Header("Medium Attack Dash")]
    public float dashSpeed = 6f;
    public float dashDuration = 0.5f;

    [Header("Cooldowns and Delays")]
    public float mediumAttackCooldown = 3f;
    public float postAttackPause = 1f;

    private bool isAttacking;
    private bool isDashing;
    private float lastMediumAttackTime = -Mathf.Infinity;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isAttacking || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= closeRange)
        {
            StartCoroutine(CloseAttack());
        }
        else if (distance <= mediumRange && Time.time >= lastMediumAttackTime + mediumAttackCooldown)
        {
            StartCoroutine(MediumAttack());
        }
        else if (distance <= longRange)
        {
            LongRangeAttack();
        }
        else
        {
            ChasePlayer(distance);
        }
    }

    void ChasePlayer(float distance)
    {
        if (distance < minChaseDistance) return;

        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)(dir * chaseSpeed * Time.deltaTime);
    }

    IEnumerator CloseAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(closeAttackCharge);

        closeAttackTrigger.SetActive(true);
        AttackTrigger damage = closeAttackTrigger.GetComponent<AttackTrigger>();
        if (damage != null)
            damage.SetDamage(closeAttackDamage);

        yield return new WaitForSeconds(0.3f);
        closeAttackTrigger.SetActive(false);

        isAttacking = false;
    }

    IEnumerator MediumAttack()
    {
        isAttacking = true;
        isDashing = true;

        lastMediumAttackTime = Time.time;
        yield return new WaitForSeconds(mediumAttackCharge);

        float elapsed = 0f;
        Vector2 dir = (player.position - transform.position).normalized;

        while (elapsed < dashDuration)
        {
            transform.position += (Vector3)(dir * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(postAttackPause);

        isAttacking = false;
    }

    void LongRangeAttack()
    {
        StartCoroutine(PostLongRangePause());
    }

    IEnumerator PostLongRangePause()
    {
        isAttacking = true;
        yield return new WaitForSeconds(postAttackPause);

        //add healing here
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthBehaviour health = other.GetComponent<HealthBehaviour>();
            if (health != null)
            {
                EntityBehaviour source = GetComponent<EntityBehaviour>();
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                int damage = isDashing ? mediumAttackDamage * 2 : mediumAttackDamage;

                DamageModel dmg = new DamageModel(source, hitPoint, damage);
                health.TakeDamage(dmg);
            }
        }
    }
}
