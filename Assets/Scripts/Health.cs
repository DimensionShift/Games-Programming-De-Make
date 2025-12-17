using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    protected int currentHealth;

    protected virtual void Start()
    {
        SetHealthToMax();
    }

    protected void SetHealthToMax()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
