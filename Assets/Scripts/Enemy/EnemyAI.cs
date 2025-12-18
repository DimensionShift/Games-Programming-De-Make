using System.Collections;
using UnityEngine;
using UnityEngine.AI;

enum States
{
    Patrol,
    DetectPlayer,
    Attack,
    Run,
    BallSearch
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] States startingState;
    [SerializeField] Vector3 patrolPoints;
    [SerializeField] Transform ballHoldTransform;

    States currentState;
    NavMeshAgent enemyAgent;
    Player player;
    GameObject ball;

    public bool hasBall;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        player = Player.Instance;
        
        currentState = startingState;
        enemyAgent.speed = moveSpeed;

        ball = GameObject.Find("Enemy Ball");
        hasBall = true;
    }

    void Update()
    {
        switch(currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.DetectPlayer:
                DetectPlayer();
                break;
            case States.Attack:
                Attack();
                break;
            case States.BallSearch:
                BallSearch();
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
            // currentState = States.DetectPlayer;
            currentState = States.Attack;
        }
    }

    void DetectPlayer()
    {
        Debug.Log("Enemy has entered Detect Player State");
        StartCoroutine(PlayerDetectedRoutine());
    }

    void Attack()
    {
        Debug.Log("Enemy has entered Attack State");

        //Attack Logic here
        // if (hasBall)
        // {
        //     ball.transform.parent = null;
        //     ball.GetComponent<Rigidbody>().isKinematic = false;
        //     ball.GetComponent<Rigidbody>().MovePosition(player.transform.position * 10f * Time.deltaTime);
            
        //     hasBall = false;
        //     currentState = States.Run;

        //     Debug.Log("Enemy has peformed their attack");
        // }
        // else
        // {
        //     Debug.Log("Can't attack, switching to ball search state");
        //     currentState = States.BallSearch;
        // }

        ball.transform.parent = null;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().MovePosition(player.transform.position * 10f * Time.deltaTime);
    }

    void Run()
    {
        Debug.Log("Enemy has entered Run State");
        enemyAgent.speed = runSpeed;

        if (!enemyAgent.hasPath)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-patrolPoints.x, patrolPoints.x), 0, Random.Range(-patrolPoints.z, patrolPoints.z));
            Debug.Log(randomPosition);

            enemyAgent.SetDestination(randomPosition);
        }

        if (!hasBall && Vector3.Distance(transform.position, player.transform.position) > 20f)
        {
            currentState = States.BallSearch;
        }
    }

    void BallSearch()
    {
        Debug.Log("Enemy has entered Ball Search State");
        enemyAgent.speed = runSpeed;

        // Find ball logic
        Vector3 ballPosition = GameObject.Find("Enemy Ball").transform.position;

        if (!enemyAgent.hasPath)
        {
            enemyAgent.SetDestination(ballPosition);
        }

        if (hasBall && Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            currentState = States.DetectPlayer;
        }
        else if (hasBall && Vector3.Distance(transform.position, player.transform.position) > 20f)
        {
            currentState = States.Patrol;
        }
    }

    IEnumerator PlayerDetectedRoutine()
    {
        transform.LookAt(player.transform);
        yield return new WaitForSeconds(1);
        currentState = States.Attack;
    }
}
