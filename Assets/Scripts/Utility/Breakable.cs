using System.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBall"))
        {
            StartCoroutine(DestroyObjectRoutine());
        }
    }

    IEnumerator DestroyObjectRoutine()
    {
        yield return StartCoroutine(GetComponent<Flash>().FlashRoutine());
        Destroy(gameObject);
    }
}
