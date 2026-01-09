using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Vector3 playerSpawnPoint;
    [SerializeField] Slider playerHealthbar;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Vector3[] ballSpawnPositions;

    List<GameObject> spawnedObjects = new List<GameObject>();

    public Player Player { get; private set; }

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
        //SpawnObjects();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Slider GetHealthbar() => playerHealthbar;

    public void SetPlayerInstance(Player player)
    {
        Player = player;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        StartCoroutine(FadeManager.Instance.FadeRoutine(0));
        
        foreach (GameObject gameObject in spawnedObjects)
        {
            Destroy(gameObject);
        }

        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < ballSpawnPositions.Length; i++)
        {
            GameObject spawnedObject = Instantiate(ballPrefab, ballSpawnPositions[i], Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
        }
    }

    public IEnumerator RestartGameSceneRoutine()
    {
        yield return StartCoroutine(FadeManager.Instance.FadeRoutine(1));
        Destroy(Player.gameObject);
        SceneLoader.Instance.LoadGameScene();
    }
}
