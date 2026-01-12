using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int ballDamage = 25;
    [SerializeField] GameObject hitVFX;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(GetComponent<Flash>().FlashRoutine());

        if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Puzzle Cube"))
        {
            audioSource.Play();
            Instantiate(hitVFX, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(ballDamage);
        }
    }
}