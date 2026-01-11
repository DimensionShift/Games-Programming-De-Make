using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    [SerializeField] int ballDamage = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth?.TakeDamage(ballDamage);
            Destroy(gameObject);
        }
    }
}