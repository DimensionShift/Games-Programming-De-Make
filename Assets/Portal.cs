using UnityEngine;

public class Portal : MonoBehaviour
{
    Door door;

    void Start()
    {
        door = GetComponentInParent<Door>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (door.isDoorOpened && other.CompareTag("Player"))
        {
            StartCoroutine(GameManager.Instance.RestartGameSceneRoutine());
        }
    }
}
