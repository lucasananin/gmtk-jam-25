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

    [Header("Attacks")]
    public GameObject Rocks;
    public Sprite[] _rockSprites;
    

    [Header("Stamina Settings")]
    public float totalStamina = 100f;
    public float staminaRecoveryPerSecond = 10f;
    public float chaseStaminaCostPerSecond = 5f;
    public float dashStaminaCost = 25f;
    public float knockDirtStaminaCost = 10f;
    public float staminaThreshold = 20f;
    public Vector2 restDurationRange = new Vector2(2f, 4f);

    [Header("Behavior Settings")]
    public float growlDistance = 10f;
    [Range(0f, 1f)] public float growlChance = 0.2f;
    public float actionPauseTime = 1.2f;
    public float runAwayTriggerDistance = 4f;
    public float runAwaySpeed = 5f;

    private float currentStamina;
    private Vector3 dashDirection;
    private float dashTimer = 0f;
    private float pauseTimer = 0f;
    private float restTimer = 0f;
    private float targetRestDuration = 0f;

    private bool hasMadeDecision = false;
    private bool hasGrowled = false;
    private int dashCount = 0;

    private enum ActionState { Chasing, Dashing, DashPrep, KnockingDirt, DirtPrep, Resting, GrowlPrep, Growling, RunningAway }
    private ActionState currentState = ActionState.Chasing;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentStamina = totalStamina;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        RegenerateStamina();

        // Emergency run away if low stamina and too close
        if (currentStamina < staminaThreshold && distance <= runAwayTriggerDistance && currentState != ActionState.RunningAway)
        {
            currentState = ActionState.RunningAway;
            restTimer = 0f;
            targetRestDuration = Random.Range(restDurationRange.x, restDurationRange.y);
            Debug.Log("Bear runs away to recover!");
            return;
        }

        // Resting after stamina drop (no threat near)
        if (currentStamina < staminaThreshold && currentState != ActionState.Resting && currentState != ActionState.RunningAway)
        {
            EnterRestingState();
            return;
        }

        if (currentState == ActionState.Chasing && distance >= growlDistance && !hasGrowled)
        {
            if (Random.value < growlChance)
            {
                currentState = ActionState.GrowlPrep;
                pauseTimer = 0f;
                return;
            }
            hasGrowled = true;
        }

        switch (currentState)
        {
            case ActionState.Chasing:
                HandleChasing(distance);
                break;
            case ActionState.GrowlPrep:
                HandlePauseThen(() => currentState = ActionState.Growling);
                break;
            case ActionState.Growling:
                Debug.Log("Bear growls!");
                currentState = ActionState.Chasing;
                break;
            case ActionState.DashPrep:
                HandlePauseThen(StartDash);
                break;
            case ActionState.Dashing:
                HandleDashing();
                break;
            case ActionState.DirtPrep:
                HandlePauseThen(KickDirt);
                break;
            case ActionState.KnockingDirt:
                Debug.Log("Bear kicks up dirt!");
                currentState = ActionState.Chasing;
                hasMadeDecision = false;
                break;
            case ActionState.Resting:
                HandleResting();
                break;
            case ActionState.RunningAway:
                HandleRunningAway(distance);
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

        if (distance <= dashDecisionRadius && !hasMadeDecision)
        {
            hasMadeDecision = true;
            int choice = Random.Range(0, 100);

            if (choice < 30 && currentStamina >= dashStaminaCost)
            {
                currentState = ActionState.DashPrep;
                pauseTimer = 0f;
            }
            else if (choice < 60 && currentStamina >= knockDirtStaminaCost)
            {
                currentState = ActionState.DirtPrep;
                pauseTimer = 0f;
            }
            else
            {
                hasMadeDecision = false;
            }
        }
    }

    void HandlePauseThen(System.Action onComplete)
    {
        pauseTimer += Time.deltaTime;
        if (pauseTimer >= actionPauseTime)
        {
            onComplete?.Invoke();
        }
    }

    void StartDash()
    {
        Vector3 baseDir = (player.position - transform.position).normalized;
        Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
        dashDirection = (baseDir + randomOffset).normalized;

        dashTimer = 0f;
        dashCount++;

        if (dashCount == 1)
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
            DecideNextDash();
        }
    }

    void DecideNextDash()
    {
        if (dashCount == 1 && Random.value < 0.7f)
        {
            StartDash(); // second dash (free)
        }
        else if (dashCount == 2 && Random.value < 0.4f)
        {
            StartDash(); // third dash (free)
        }
        else
        {
            if (dashCount == 3)
                currentStamina = 0f;

            dashCount = 0;
            currentState = ActionState.Chasing;
            hasMadeDecision = false;
        }
    }

    void KickDirt()
    {
        currentStamina -= knockDirtStaminaCost;
        currentState = ActionState.KnockingDirt;

        int rockCount = Random.Range(2, 6);
        Vector3 baseDir = (player.position - transform.position).normalized;

        for (int i = 0; i < rockCount; i++)
        {
            Vector3 spread = Quaternion.Euler(0, Random.Range(-90f, 90f), 0) * baseDir;
            GameObject rock = Instantiate(Rocks, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            RockProjectile rockProj = rock.GetComponent<RockProjectile>();
            if (rockProj != null)
            {
                rockProj.Launch(spread, _rockSprites[Random.Range(0, _rockSprites.Length)]);
            }
        }

        Invoke(nameof(ReturnToChaseAfterKick), 0.5f); 
    }

    void ReturnToChaseAfterKick()
    {
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

    void HandleRunningAway(float distance)
    {
        Vector3 directionAway = (transform.position - player.position).normalized;
        transform.position += directionAway * runAwaySpeed * Time.deltaTime;

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

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
