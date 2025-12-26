using UnityEngine;

public class PuzzleCube : MonoBehaviour
{
    [SerializeField] int orderInSequence;
    [SerializeField] Material hitMaterial;

    PuzzleManager puzzleManager;
    MeshRenderer meshRenderer;
    Material defaultMaterial;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        puzzleManager = PuzzleManager.Instance;
        defaultMaterial = meshRenderer.material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (puzzleManager.isPuzzleActive && collision.gameObject.CompareTag("PlayerBall"))
        {
            puzzleManager.CubeHit(orderInSequence);
            meshRenderer.material = hitMaterial;
        }
    }
    public void ResetMaterial()
    {
        meshRenderer.material = defaultMaterial;
    }
}
