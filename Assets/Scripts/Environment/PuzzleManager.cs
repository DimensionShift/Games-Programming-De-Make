using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [SerializeField] List<int> puzzleSolutionSequence = new List<int>();
    //[SerializeField] GameObject[] puzzleGameObjects;
    public bool isPuzzleSolved { get ; private set; } = false;
    
    [field:SerializeField] List<GameObject> puzzleGameObjects = new List<GameObject>();
    List<int> playerHitSequence = new List<int>();

    public event Action OnPuzzleSolved;

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
        RestartPuzzle();
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
            isPuzzleSolved = true;
            OnPuzzleSolved?.Invoke();
        }
    }

    public void RestartPuzzle()
    {
        isPuzzleSolved = false;
        playerHitSequence.Clear();

        foreach(GameObject gameObject in puzzleGameObjects)
        {
            gameObject.GetComponent<PuzzleCube>().ResetCube();
        }
    }

    public bool PuzzleSolved => isPuzzleSolved;

    public void AddToPuzzleObjects(GameObject puzzleCube)
    {
        puzzleGameObjects.Add(puzzleCube);
    }

    public void ClearPuzzleObjects()
    {
        puzzleGameObjects.Clear();
    }
}
