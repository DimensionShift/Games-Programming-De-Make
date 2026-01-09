using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool isDoorOpenedWithKey;
    [SerializeField] MeshRenderer meshRenderer; 
    [SerializeField] Material doorLockedMaterial;
    [SerializeField] Material doorOpenedMaterial;

    public bool isDoorOpened { get; private set; } = false;

    PuzzleManager puzzleManager;

    void Start()
    {
        puzzleManager = PuzzleManager.Instance;
        puzzleManager.OnPuzzleSolved += OpenDoors;
        isDoorOpened = false;
        GetComponent<BoxCollider>().enabled = true;
        meshRenderer.material = doorLockedMaterial;
    }

    void OnDestroy()
    {
        puzzleManager.OnPuzzleSolved -= OpenDoors;
    }

    public void OpenDoors()
    {
        Lerper[] lerpers = GetComponentsInChildren<Lerper>();

        foreach (Lerper lerper in lerpers)
        {
            lerper.SetCanLerp(true);
        }

        isDoorOpened = true;
        meshRenderer.material = doorOpenedMaterial;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isDoorOpenedWithKey) return;

        if (collision.gameObject.CompareTag("Door Key"))
        {
            GetComponent<BoxCollider>().enabled = false;
            OpenDoors();
            Destroy(collision.gameObject);
        }
    }
}
