using System.Collections;
using UnityEngine;

public class ThrowingEnemy : EnemyAI
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform ballHoldTransform;
    [SerializeField] float detectPlayerWaitTime;

    Coroutine playerDetectedCoroutine;
    bool canAttack;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        switch(currentState)
        {
            case States.Attack:
                Attack();
                break;
        }
    }

    protected override void Patrol()
    {
        base.Patrol();

        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            currentState = States.Attack;
        }
    }

    protected void Attack()
    {
        enemyAgent.ResetPath();
        transform.LookAt(player.transform);

        if (playerDetectedCoroutine == null)
        {
            playerDetectedCoroutine = StartCoroutine(DetectPlayerRoutine());
        }

        if (canAttack)
        {
            GameObject ball = Instantiate(ballPrefab, ballHoldTransform.position, Quaternion.identity);
            Vector3 distanceToPlayer = (player.transform.position + (Vector3.up * 1f) - ball.transform.position).normalized;
            ball.GetComponent<Rigidbody>().linearVelocity = distanceToPlayer * 50f;

            Destroy(ball, 2);
            canAttack = false;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            currentState = States.Patrol;
        }
    }

    IEnumerator DetectPlayerRoutine()
    {
        yield return new WaitForSeconds(detectPlayerWaitTime);
        canAttack = true;
        playerDetectedCoroutine = null;
    }
}
