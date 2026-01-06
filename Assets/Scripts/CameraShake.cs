using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeForce;

    public IEnumerator CameraShakeRoutine()
    {
        Vector3 originalCameraPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeForce;
            float yOffset = Random.Range(-1f, 1f) * shakeForce;

            transform.localPosition = new Vector3(xOffset, yOffset, transform.localScale.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalCameraPosition;
    }
}
