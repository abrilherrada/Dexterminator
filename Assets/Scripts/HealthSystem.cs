using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private PCData data;
    [SerializeField] protected Animator animator;

    [SerializeField] private float invincibilityTime;
    private bool isInvincible;
    protected float health;

    public event Action OnPCDeath;
    public event Action OnHealthChange;


    public void Init()
    {
        health = data.maxHealth;
    }

    public float GetHealth() => health;

    public void SetHealth(float currentHealth)
    {
        health = currentHealth;
        OnHealthChange?.Invoke();
    }

    public void GetHealed(float healingPoints)
    {
        if (health + healingPoints > data.maxHealth)
        {
            health = data.maxHealth;
        }
        else if (health + healingPoints <= data.maxHealth && health > 0)
        {
            health += healingPoints;
        }

        OnHealthChange?.Invoke();
    }

    public void TakeDamage(float damagePoints)
    {
        if(isInvincible)
        {
            return;
        }

        health -= damagePoints;
        animator.SetTrigger("isTakingDamage"); //sacar

        if (health - damagePoints < 0)
        {
            health = 0;
            animator.SetInteger("health", 0);
            return;
        }

        OnHealthChange?.Invoke();
    }

    public virtual void Die()
    {
        isInvincible = true;
        OnPCDeath?.Invoke();
    }
}
