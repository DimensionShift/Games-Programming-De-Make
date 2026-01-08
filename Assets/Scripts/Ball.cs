using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] int ballDamage = 20;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            Instantiate(hitVFX, transform);
            enemyHealth?.TakeDamage(ballDamage);
        }
    }
}
