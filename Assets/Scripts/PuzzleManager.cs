using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [SerializeField] List<int> puzzleSolutionSequence = new List<int>();
    [SerializeField] GameObject[] puzzleGameObjects;

    public bool isPuzzleSolved {get ; private set;} = false;
    List<int> playerHitSequence = new List<int>();

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

    void Start()
    {
        isPuzzleSolved = false;
    }

    public void CubeHit(int hitCube)
    {
        playerHitSequence.Add(hitCube);

        for (int i = 0; i < playerHitSequence.Count; i ++)
        {
            if (playerHitSequence[i] != puzzleSolutionSequence[i])
            {
                RestartPuzzle();
                return;
            }
        }

        if (playerHitSequence.Count == puzzleSolutionSequence.Count)
        {
            // door open logic
            Debug.Log("Puzzle Solved");
            isPuzzleSolved = true;
        }
    }

    void RestartPuzzle()
    {
        Debug.Log("Puzzle Restarted");
        playerHitSequence.Clear();

        foreach(GameObject gameObject in puzzleGameObjects)
        {
            gameObject.GetComponent<PuzzleCube>().ResetCube();
        }
    }

    public bool PuzzleSolved => isPuzzleSolved;
}
