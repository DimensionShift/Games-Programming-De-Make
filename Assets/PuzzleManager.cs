using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [SerializeField] List<int> puzzleSolutionSequence = new List<int>();
    [SerializeField] GameObject[] puzzleGameObjects;

    List<int> playerHitSequence = new List<int>();

    public bool isPuzzleActive = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CubeHit(int hitCube)
    {
        playerHitSequence.Add(hitCube);

        for (int i = 0; i < playerHitSequence.Count; i ++)
        {
            if (playerHitSequence[i] != puzzleSolutionSequence[i])
            {
                StartCoroutine(RestartPuzzle());
                return;
            }
        }

        if (playerHitSequence.Count == puzzleSolutionSequence.Count)
        {
            // door open logic
            Debug.Log("Puzzle Solved");
            isPuzzleActive = false;
        }
    }

    IEnumerator RestartPuzzle()
    {
        Debug.Log("Puzzle Restarted");
        playerHitSequence.Clear();

        yield return null;

        foreach(GameObject gameObject in puzzleGameObjects)
        {
            gameObject.GetComponent<PuzzleCube>().ResetMaterial();
        }
    }
}
