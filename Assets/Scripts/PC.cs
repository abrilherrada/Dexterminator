using UnityEngine;

public abstract class PC : Entity
{
    [SerializeField] public HealthSystem healthSystem;

    [SerializeField] private PCData data;

    [SerializeField] protected Animator animator;

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

    public virtual void Die()
    {
        healthSystem.Die();
    }

    protected virtual void OnPCDeathHandler()
    {
        Debug.Log("PC dead.");
    }

    private void OnDestroy()
    {
        healthSystem.OnPCDeath -= OnPCDeathHandler;
    }

    private void Awake()
    {
        healthSystem.Init();
        healthSystem.OnPCDeath += OnPCDeathHandler;
    }
}
