using UnityEngine;

public class FlyingEnemy : EnemyAI
{
    [SerializeField] float runSpeed = 10f;

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
        enemyAgent.speed = moveSpeed;

        base.Patrol();
    }

    protected override void Attack()
    {
        base.Attack();

        enemyAgent.speed = runSpeed;
        enemyAgent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(contactDamage);
            Destroy(gameObject);
        }
    }
}
