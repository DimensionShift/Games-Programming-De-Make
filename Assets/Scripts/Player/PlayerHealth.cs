using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public bool isDead { get; private set; } = false;

    Slider healthbarSlider;
    CameraShake cameraShake;

    Coroutine cameraShakeCoroutine;

    protected override void Start()
    {
        base.Start();

        healthbarSlider = GameManager.Instance.GetHealthbar();
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

        if (cameraShakeCoroutine == null)
        {
            cameraShakeCoroutine = StartCoroutine(StartCameraShakeRoutine());
        }
    }

    public void RestoreHealth(int amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        UpdateHealthbar();
    }

    public override void Die()
    {
        isDead = true;
        StartCoroutine(GameManager.Instance.RestartGameSceneRoutine());
    }

    IEnumerator StartCameraShakeRoutine()
    {
        yield return StartCoroutine(cameraShake.CameraShakeRoutine());
        cameraShakeCoroutine = null;
    }
}