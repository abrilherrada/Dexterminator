using UnityEngine;

public abstract class PC : Entity
{
    [SerializeField] private PCData data;

    [SerializeField] protected Animator animator;

    protected float health;

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
    }

    public void TakeDamage(float damagePoints)
    {
        health -= damagePoints;
        animator.SetTrigger("isTakingDamage");

        if (health - damagePoints < 0)
        {
            health = 0;
            animator.SetInteger("health", 0);
        }
    }

    public float GetHealth() => health;

    protected void Move(Vector3 direction)
    {
        transform.Translate(data.movementSpeed * Time.deltaTime * direction, Space.World);
        animator.SetBool("isMoving", true);
    }

    protected void Look(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, data.rotationSpeed * Time.deltaTime);
    }

    public abstract void Die();

    private void Start()
    {
        health = data.maxHealth;
    }
}
