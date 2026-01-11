using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int ballDamage = 25;
    [SerializeField] GameObject hitVFX;

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(GetComponent<Flash>().FlashRoutine());

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