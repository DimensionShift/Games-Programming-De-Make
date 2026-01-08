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
    }

    protected override void Attack()
    {
        base.Attack();
        
        if (playerDetectedCoroutine == null)
        {
            playerDetectedCoroutine = StartCoroutine(DetectPlayerRoutine());
        }

        if (canAttack)
        {
            GameObject ball = Instantiate(ballPrefab, ballHoldTransform.position, Quaternion.identity);
            Vector3 distanceToPlayer = (player.transform.position - ball.transform.position).normalized;
            ball.GetComponent<Rigidbody>().linearVelocity = distanceToPlayer * 50f;

            Destroy(ball, 2);
            canAttack = false;
        }
    }

    IEnumerator DetectPlayerRoutine()
    {
        yield return new WaitForSeconds(detectPlayerWaitTime);
        canAttack = true;
        playerDetectedCoroutine = null;
    }
}
