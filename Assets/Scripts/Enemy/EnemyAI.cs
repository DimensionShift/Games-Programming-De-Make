using System.Collections;
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

    States currentState;
    NavMeshAgent enemyAgent;
    Player player;

    Coroutine currentCoRoutine;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        player = Player.Instance;
        
        currentState = States.Patrol;
        enemyAgent.speed = moveSpeed;
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

        if (!enemyAgent.hasPath)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-patrolPoints.x, patrolPoints.x), 0, Random.Range(-patrolPoints.z, patrolPoints.z));
            enemyAgent.SetDestination(randomPosition);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            enemyAgent.ResetPath();

            if (currentCoRoutine == null)
            {
                currentCoRoutine = StartCoroutine(PlayerDetectedRoutine());
            }
        }
    }

    void Attack()
    {
        Debug.Log("Enemy has entered Attack State");

        // Attack Logic
        GameObject ball = Instantiate(ballPrefab, ballHoldTransform.position, Quaternion.identity);

        currentState = States.Run;
    }

    void Run()
    {
        Debug.Log("Enemy has entered Run State");
        enemyAgent.ResetPath();
        enemyAgent.speed = runSpeed;

        Vector3 randomPosition = new Vector3(Random.Range(-patrolPoints.x, patrolPoints.x), 0, Random.Range(-patrolPoints.z, patrolPoints.z));
        enemyAgent.SetDestination(randomPosition);

        currentState = States.Patrol;
    }

    IEnumerator PlayerDetectedRoutine()
    {
        transform.LookAt(player.transform);
        
        // Add some form of indicator that the player has been detected --> '!'
        yield return new WaitForSeconds(2);

        if (Vector3.Distance(transform.position, player.transform.position) > 20f)
        {
            // Add some form of indicator that the player has been lost --> '?'
            currentState = States.Patrol;
        }
        else
        {
            currentState = States.Attack;
        }
    }
}
