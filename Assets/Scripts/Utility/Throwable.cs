using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] int damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Environment"))
        {
            collision.gameObject.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
