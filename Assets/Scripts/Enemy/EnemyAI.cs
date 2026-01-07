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
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] BoxCollider patrolArea;
    [SerializeField] float patrolAreaDuration;

    protected States currentState;
    protected NavMeshAgent enemyAgent;
    protected Player player;

    bool isWaiting = false;

    protected virtual void Start()
    {
        player = GameManager.Instance.Player;
        enemyAgent = GetComponent<NavMeshAgent>();
        
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
}
