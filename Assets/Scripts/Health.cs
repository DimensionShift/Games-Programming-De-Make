using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider healthBarSlider;
    [SerializeField] int maxHealth = 100;

    int currentHealth;

    void Start()
    {
        SetHealthToMax();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void UpdateHealthbar()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    void SetHealthToMax()
    {
        currentHealth = maxHealth;
        UpdateHealthbar();
    }
}
