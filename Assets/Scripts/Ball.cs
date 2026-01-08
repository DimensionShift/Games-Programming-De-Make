using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int ballDamage = 20;
    [SerializeField] GameObject hitVFX;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Instantiate(hitVFX, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(ballDamage);
        }
    }
}