using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    Slider healthbarSlider;
    CameraShake cameraShake;
    SceneLoader sceneLoader;

    public bool isDead { get; private set; } = false;

    protected override void Start()
    {
        base.Start();

        healthbarSlider = GameManager.Instance.GetHealthbar();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        sceneLoader = SceneLoader.Instance;

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
        isDead = true;
        StartCoroutine(GameManager.Instance.RestartGameSceneRoutine());
    }
}