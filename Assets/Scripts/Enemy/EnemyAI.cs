using UnityEngine;
using UnityEngine.AI;

enum States
{
    Patrol,
    DetectPlayer,
    Attack,
    Run,
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] Vector3 patrolPoints;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform ballHoldTransform;
    [SerializeField] float timeBeforeAttacking = 5f;

    States currentState;
    NavMeshAgent enemyAgent;
    Player player;

    bool canAttack = true;
    float timer;

    void Start()
    {
        player = GameManager.Instance.Player;
        enemyAgent = GetComponent<NavMeshAgent>();
        
        currentState = States.Patrol;
        enemyAgent.speed = moveSpeed;

        timer = timeBeforeAttacking;
    }

    void Update()
    {
        switch(currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Attack:
                Attack();
                break;
            case States.Run:
                Run();
                break;
            default:
                currentState = States.Patrol;
                break;
        }
    }

    void Patrol()
    {
        Debug.Log("Enemy has entered Patrol State");
        enemyAgent.speed = moveSpeed;
        canAttack = true;

        if (!enemyAgent.hasPath)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-patrolPoints.x, patrolPoints.x), 0, Random.Range(-patrolPoints.z, patrolPoints.z));
            enemyAgent.SetDestination(randomPosition);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            enemyAgent.ResetPath();
            transform.LookAt(player.transform);

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                currentState = States.Attack;
                timer = timeBeforeAttacking;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Enemy has entered Attack State");

        // Attack Logic
        if (canAttack)
        {
            GameObject ball = Instantiate(ballPrefab, ballHoldTransform.position, Quaternion.identity);
            Vector3 distanceToPlayer = (player.transform.position + (Vector3.up * 1f) - ball.transform.position).normalized;
            ball.GetComponent<Rigidbody>().linearVelocity = distanceToPlayer * 50f;

            Destroy(ball, 5);
            canAttack = false;
        }
        else
        {
            currentState = States.Run;
        }
    }

    void Run()
    {
        Debug.Log("Enemy has entered Run State");
        enemyAgent.speed = runSpeed;

        if (!enemyAgent.hasPath)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-patrolPoints.x, patrolPoints.x), 0, Random.Range(-patrolPoints.z, patrolPoints.z));
            enemyAgent.SetDestination(randomPosition);
        }

        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < enemyAgent.stoppingDistance)
        {
            enemyAgent.ResetPath();
            currentState = States.Patrol;
        }
    }
}
