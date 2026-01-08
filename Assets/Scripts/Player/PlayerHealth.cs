using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
<<<<<<< HEAD
<<<<<<< HEAD
    Slider healthbarSlider;
    CameraShake cameraShake;
    SceneLoader sceneLoader;

    public bool isDead { get; private set; } = false;
=======
    [SerializeField] Slider healthbarSlider;
>>>>>>> parent of 28d1a1b (Adjusted VFX + Added Camera Shake)
=======
    [SerializeField] Slider healthbarSlider;
>>>>>>> parent of 28d1a1b (Adjusted VFX + Added Camera Shake)

    protected override void Start()
    {
        base.Start();

<<<<<<< HEAD
<<<<<<< HEAD
        healthbarSlider = GameManager.Instance.GetHealthbar();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        sceneLoader = SceneLoader.Instance;

=======
>>>>>>> parent of 28d1a1b (Adjusted VFX + Added Camera Shake)
=======
>>>>>>> parent of 28d1a1b (Adjusted VFX + Added Camera Shake)
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
        isDead = true;
        StartCoroutine(GameManager.Instance.RestartGameSceneRoutine());
    }
}