using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitVFX, transform);
            Destroy(collision.gameObject);
        }
    }
}
