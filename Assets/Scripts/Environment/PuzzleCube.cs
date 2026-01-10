using UnityEngine;

public class PuzzleCube : MonoBehaviour
{
    [SerializeField] int orderInSequence;
    [SerializeField] Material hitMaterial;
    [SerializeField] Color cubeColour;
    [SerializeField] float lerpDuration = 1f;

    PuzzleManager puzzleManager;
    MeshRenderer meshRenderer;
    Material defaultMaterial;

    float elapsedTime = 0f;
    bool isHit;
    bool isLerping;

    void Start()
    {
        puzzleManager = PuzzleManager.Instance;
        meshRenderer = GetComponent<MeshRenderer>();

        defaultMaterial = meshRenderer.material;

        isHit = false;
        isLerping = false;

        ResetCube();

        puzzleManager.AddToPuzzleObjects(gameObject);
    }

    void Update()
    {
        if (isHit && isLerping)
        {
            LerpCubeColor();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!puzzleManager.isPuzzleSolved && !isHit && collision.gameObject.CompareTag("PlayerBall"))
        {
            isHit = true;
            puzzleManager.CubeHit(orderInSequence);
            isLerping = true;
        }
    }

    public void ResetCube()
    {
        meshRenderer.material = defaultMaterial;
        elapsedTime = 0f;
        isHit = false;
    }

    void LerpCubeColor()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < lerpDuration)
        {
            float tempValue = elapsedTime / lerpDuration;
            defaultMaterial.color = Color.Lerp(Color.black, cubeColour, tempValue);
        }
        else
        {
            isLerping = false;
            meshRenderer.material = hitMaterial;
        }
    }
}
