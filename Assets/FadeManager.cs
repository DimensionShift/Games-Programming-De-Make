using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [SerializeField] Image fadeImage;
    [SerializeField] float fadeSpeed = 1f;

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

    public IEnumerator FadeRoutine(float targetValue)
    {
        while (!Mathf.Approximately(fadeImage.color.a, targetValue))
        {
            float alpha = Mathf.MoveTowards(fadeImage.color.a, targetValue, fadeSpeed * Time.deltaTime);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
    }
}