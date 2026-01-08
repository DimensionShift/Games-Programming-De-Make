using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] Slider healthbarSlider;

    CameraShake cameraShake;

    protected override void Start()
    {
        base.Start();

        cameraShake = Camera.main.GetComponent<CameraShake>();

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
        StartCoroutine(cameraShake.CameraShakeRoutine());
    }

    protected override void Die()
    {
        // To Implement once enemies can attack
        Debug.Log("Player has died");
    }
}
