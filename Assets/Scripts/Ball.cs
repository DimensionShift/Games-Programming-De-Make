using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] int ballDamage = 20;

    Vector3 spawnPosition;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            Instantiate(hitVFX, collision.transform.position + (Vector3.up * 1.5f), Quaternion.identity);
            enemyHealth?.TakeDamage(ballDamage);
        }
        else
        {
            Instantiate(hitVFX, spawnPosition, Quaternion.identity);
        }
    }
}
