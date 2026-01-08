using UnityEngine;

public class EnemyHealth : Health
{
    Flash flash;
    Coroutine flashCoroutine;

    protected override void Start()
    {
        base.Start();
        flash = GetComponent<Flash>();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        DamageFlash();
        flashCoroutine = null;
        // Add material flash to enemy when taking damage
    }

    void DamageFlash()
    {
        if (flashCoroutine == null)
        {
            flashCoroutine = StartCoroutine(flash.FlashRoutine());
        }
    }
}