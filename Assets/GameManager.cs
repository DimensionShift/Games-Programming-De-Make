using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Vector3 playerSpawnPoint;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Slider playerHealthbar;

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
        StartCoroutine(FadeManager.Instance.FadeRoutine(0));
    }

    public IEnumerator RestartGameSceneRoutine()
    {
        yield return StartCoroutine(FadeManager.Instance.FadeRoutine(1));
        Destroy(Player.gameObject);
        SceneLoader.Instance.LoadGameScene();
    }
}
