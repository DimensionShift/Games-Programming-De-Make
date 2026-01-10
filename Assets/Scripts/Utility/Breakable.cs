using UnityEngine;

public class Breakable : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBall"))
        {
            Destroy(gameObject);
        }
    }
}
