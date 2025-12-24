using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] Slider healthbarSlider;

    protected override void Start()
    {
        base.Start();

        SetHealthToMax();
        UpdateHealthbar();
    }

    public void UpdateHealthbar()
    {
        healthbarSlider.maxValue = maxHealth;
        healthbarSlider.value = currentHealth;
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        UpdateHealthbar();
    }

    protected override void Die()
    {
        // To Implement once enemies can attack
        Debug.Log("Player has died");
    }
}
