using UnityEngine;

public class UIPrompt : MonoBehaviour
{
    [SerializeField] GameObject promptText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayPrompt(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayPrompt(false);
        }
    }

    void DisplayPrompt(bool shouldDisplay)
    {
        promptText.SetActive(shouldDisplay);
    }
}
