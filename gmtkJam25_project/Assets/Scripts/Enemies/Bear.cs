using UnityEngine;

public class Bear : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Movement Settings")]
    public float chaseSpeed = 3f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    public float dashDecisionRadius = 6f;
    public float attackRange = 1.5f;
    public float growlDistance = 10f;

    [Header("Stamina Settings")]
    public float totalStamina = 100f;
    public float staminaRecoveryPerSecond = 10f;
    public float chaseStaminaCostPerSecond = 5f;
    public float dashStaminaCost = 25f;
    public float knockDirtStaminaCost = 10f;
    public float attackStaminaCost = 15f;
    public float staminaThreshold = 20f;
    public Vector2 restDurationRange = new Vector2(2f, 4f);

    [Header("Attack Cooldown")]
    public float attackCooldown = 2f;

    public float currentStamina;
    private float attackTimer = 0f;

    private enum ActionState { Chasing, Dashing, Approaching, KnockingDirt, Attacking, Resting }
    private ActionState currentState = ActionState.Chasing;

    private Vector3 dashDirection;
    private float dashTimer = 0f;
    private bool hasMadeDecision = false;
    private bool hasGrowled = false;

    private float restTimer = 0f;
    private float targetRestDuration = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentStamina = totalStamina;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        HandleGrowl(distance);
        RegenerateStamina();
        attackTimer += Time.deltaTime;

        if (currentStamina < staminaThreshold && currentState != ActionState.Resting)
        {
            EnterRestingState();
        }

        switch (currentState)
        {
            case ActionState.Chasing:
                HandleChasing(distance);
                break;
            case ActionState.Dashing:
                HandleDashing();
                break;
            case ActionState.Approaching:
                HandleApproaching();
                break;
            case ActionState.KnockingDirt:
                HandleKnockingDirt();
                break;
            case ActionState.Attacking:
                HandleAttacking(distance);
                break;
            case ActionState.Resting:
                HandleResting();
                break;
        }
    }

    void HandleChasing(float distance)
    {
        if (currentStamina >= chaseStaminaCostPerSecond * Time.deltaTime)
        {
            MoveTowards(player.position, chaseSpeed);
            currentStamina -= chaseStaminaCostPerSecond * Time.deltaTime;
        }

        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            currentState = ActionState.Attacking;
        }
        else if (distance <= dashDecisionRadius && !hasMadeDecision)
        {
            int choice = Random.Range(0, 100);
            hasMadeDecision = true;

            if (choice < 30 && currentStamina >= dashStaminaCost)
            {
                StartDash();
            }
            else if (choice < 60 && currentStamina >= knockDirtStaminaCost)
            {
                currentStamina -= knockDirtStaminaCost;
                currentState = ActionState.KnockingDirt;
            }
            else
            {
                currentState = ActionState.Approaching;
            }
        }
    }

    void StartDash()
    {
        dashDirection = (player.position - transform.position).normalized;
        dashTimer = 0f;
        currentStamina -= dashStaminaCost;
        currentState = ActionState.Dashing;
    }

    void HandleDashing()
    {
        dashTimer += Time.deltaTime;

        if (dashTimer < dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
        }
        else
        {
            currentState = ActionState.Chasing;
            hasMadeDecision = false;
        }
    }

    void HandleApproaching()
    {
        if (currentStamina >= chaseStaminaCostPerSecond * Time.deltaTime)
        {
            MoveTowards(player.position, chaseSpeed);
            currentStamina -= chaseStaminaCostPerSecond * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, player.position) <= attackRange && attackTimer >= attackCooldown)
        {
            currentState = ActionState.Attacking;
        }
    }

    void HandleKnockingDirt()
    {
        Debug.Log("Bear kicks up dirt!");
        currentState = ActionState.Chasing;
        hasMadeDecision = false;
    }

    void HandleAttacking(float distance)
    {
        if (currentStamina >= attackStaminaCost)
        {
            Debug.Log("Bear attacks!");
            currentStamina -= attackStaminaCost;
            attackTimer = 0f;
        }

        currentState = ActionState.Chasing;
        hasMadeDecision = false;
    }

    void EnterRestingState()
    {
        restTimer = 0f;
        targetRestDuration = Random.Range(restDurationRange.x, restDurationRange.y);
        currentState = ActionState.Resting;
        Debug.Log("Bear is resting...");
    }

    void HandleResting()
    {
        restTimer += Time.deltaTime;

        if (restTimer >= targetRestDuration)
        {
            currentState = ActionState.Chasing;
            hasMadeDecision = false;
        }
    }

    void RegenerateStamina()
    {
        currentStamina += staminaRecoveryPerSecond * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, totalStamina);
    }

    void HandleGrowl(float distance)
    {
        if (distance > growlDistance)
        {
            hasGrowled = false;
        }
        else if (!hasGrowled)
        {
            Debug.Log("Bear growls!");
            hasGrowled = true;
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
