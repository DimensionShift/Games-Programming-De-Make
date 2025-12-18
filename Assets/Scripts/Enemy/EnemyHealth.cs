using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Die()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        // Add material flash to enemy when taking damage
    }
}