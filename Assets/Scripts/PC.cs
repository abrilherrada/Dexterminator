using UnityEngine;

public class PC : Entity
{
    [SerializeField] protected Animator animator;

    protected float health;
    protected float maxHealth = 100;

    protected float movementSpeed;
    protected float rotationSpeed;

    public void GetHealed(float healingPoints)
    {
        if (health + healingPoints > maxHealth)
        {
            health = maxHealth;
        }
        else if (health + healingPoints <= maxHealth && health > 0)
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
        transform.Translate(movementSpeed * Time.deltaTime * direction, Space.World);
        animator.SetBool("isMoving", true);
    }

    protected void Look(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Start()
    {
        health = maxHealth;
    }
}
