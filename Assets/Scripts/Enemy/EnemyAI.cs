using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    Patrol,
    Attack
}

public abstract class EnemyAI : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected int contactDamage = 10;
    [SerializeField] int detectionRadius = 20;
    [SerializeField] BoxCollider patrolArea;
    [SerializeField] float patrolAreaDuration = 1;

    [field:SerializeField] protected Player player;
    [field:SerializeField] protected PlayerHealth playerHealth;

    protected States currentState;
    protected NavMeshAgent enemyAgent;

    bool isWaiting = false;

    protected virtual void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(FindPlayerReferenceRoutine());
        
        currentState = States.Patrol;
        enemyAgent.speed = moveSpeed;

        enemyAgent.SetDestination(GetRandomPatrolPoint());
    }

    protected virtual void Update()
    {
        switch(currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Attack:
                Attack();
                break;
            default:
                currentState = States.Patrol;
                break;
        }
    }

    protected virtual void Patrol()
    {
        if (!enemyAgent.hasPath && !isWaiting)
        {
            Vector3 randomPosition = GetRandomPatrolPoint();
            enemyAgent.SetDestination(randomPosition);
            isWaiting = true;
        }

        if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
        {
            enemyAgent.ResetPath();
            StartCoroutine(WaitAtPatrolPointRoutine());
        }

        if (player == null || playerHealth.isDead) return;

        if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
        {
            currentState = States.Attack;
        }
    }

    protected virtual void Attack()
    {
        if (player == null || playerHealth.isDead) return;

        enemyAgent.ResetPath();

        Vector3 targetPosition = player.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
        {
            currentState = States.Patrol;
        }
    }

    protected Vector3 GetRandomPatrolPoint()
    {
        Bounds patrolBounds = patrolArea.bounds;
        Vector3 randomPatrolPosition = new Vector3(Random.Range(patrolBounds.min.x, patrolBounds.max.x), patrolBounds.center.y, Random.Range(patrolBounds.min.z, patrolBounds.max.z));
        NavMesh.SamplePosition(randomPatrolPosition, out NavMeshHit hit, 2f, NavMesh.AllAreas);

        return hit.position;
    }

    IEnumerator WaitAtPatrolPointRoutine()
    {
        yield return new WaitForSeconds(patrolAreaDuration);
        isWaiting = false;
    }

    IEnumerator FindPlayerReferenceRoutine()
    {
        yield return null;

        player = GameManager.Instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public int GetContactDamage() => contactDamage;
}
