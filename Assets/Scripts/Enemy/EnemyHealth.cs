using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject DeathVFX;

    Flash flash;
    Coroutine flashCoroutine;

    protected override void Start()
    {
        base.Start();
        flash = GetComponent<Flash>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        if (flash != null)
        {
            DamageFlash();
            flashCoroutine = null;
        }
    }

    protected override void Die()
    {
        Instantiate(DeathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void DamageFlash()
    {
        if (flashCoroutine == null)
        {
            flashCoroutine = StartCoroutine(flash.FlashRoutine());
        }
    }
}