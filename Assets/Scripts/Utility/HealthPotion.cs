using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] int healthRestored = 50;

    public void RestoreHealth()
    {
        GameManager.Instance.Player.GetComponent<PlayerHealth>().RestoreHealth(healthRestored);
        Destroy(gameObject);
    }
}
