using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material flashMaterial;
    [SerializeField] float flashDuration;

    Material defaultMaterial;

    void Start()
    {
        defaultMaterial = meshRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        meshRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        meshRenderer.material = defaultMaterial;
    }
}
