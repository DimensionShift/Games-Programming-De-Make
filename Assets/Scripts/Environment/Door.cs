using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool openDoorForTesting = false;

    PuzzleManager puzzleManager;

    void Start()
    {
        puzzleManager = PuzzleManager.Instance;
    }

    void Update()
    {
        if (puzzleManager.isPuzzleSolved || openDoorForTesting)
        {
            OpenDoors();
        }
    }

    public void OpenDoors()
    {
        Lerper[] lerpers = GetComponentsInChildren<Lerper>();

        foreach (Lerper lerper in lerpers)
        {
            lerper.SetCanLerp(true);
        }
    }
}
