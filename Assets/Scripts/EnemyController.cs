using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Spider1,
    Spider2
}

public class EnemyController : PC
{
    [SerializeField] private EnemyData enemyData;

    [SerializeField] private PostProcessingController postProcessingController;
    [SerializeField] private PlayerController player;

    [SerializeField] private Rigidbody enemyRigidbody;
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private Transform target;
    [SerializeField] private Rock rockToThrow;
    [SerializeField] private Transform throwingPoint;
    [SerializeField] private float timeToThrow = 2f;
    private float timeLeftToThrow;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastRadius = 0.2f;
    [SerializeField] private float raycastMaxDistance = 6f;
    [SerializeField] private LayerMask raycastLayers;

    [SerializeField] private float distanceToStartChasing = 5f;
    [SerializeField] private float distanceToStartThrowing = 4f;

    private float pushForce = 2.5f;

    private void SetEnemyAction(EnemyTypes enemyType, Vector3 direction)
    {
        switch(enemyType)
        {
            case EnemyTypes.Spider1:
                Chase(direction);
                break;
            case EnemyTypes.Spider2:
                Throw(direction);
                break;
            default:
                break;
        }
    }

    private bool IsTargetInSight()
    {
        bool isHitting = Physics.SphereCast(raycastOrigin.position, raycastRadius, raycastOrigin.forward, out RaycastHit target, raycastMaxDistance, raycastLayers);

        if(isHitting && target.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Chase(Vector3 direction)
    {
        Look(direction);

        if (distanceToStartChasing >= direction.magnitude && healthSystem.GetHealth() > 0 && IsTargetInSight() && direction != Vector3.zero)
        {
            Move(direction);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void Throw(Vector3 direction)
    {
        Look(direction);

        if (IsTargetInSight() && distanceToStartThrowing >= direction.magnitude && timeLeftToThrow <= 0)
        {
            animator.SetBool("isThrowing", true);
            Instantiate(rockToThrow, throwingPoint.position, throwingPoint.rotation);
            timeLeftToThrow = timeToThrow;
        }
        else
        {
        animator.SetBool("isThrowing", false);
        }
    }

    public override void Die()
    {
        animator.SetBool("isThrowing", false);
        GameManager.Instance.AddScore(enemyData.pointsForKill);
        player.CollectEnemy();
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void GetPushed()
    {
        Vector3 forceVector = transform.forward * pushForce;
        enemyRigidbody.AddForce(-forceVector, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (healthSystem.GetHealth() > 0)
            {
                if(player.GetIsSpinning())
                {
                    healthSystem.TakeDamage(enemyData.damageTaken);
                    GetPushed();
                    if (healthSystem.GetHealth() <= 0)
                    {
                        Die();
                    }
                    return;
                }

                animator.SetTrigger("isAttacking");
                player.CanTakeDamage(enemyData.damageDone);
                postProcessingController.UseDamagedEffect();
                Debug.Log($"player health: {player.healthSystem.GetHealth()}");
            }
        }

        if (collision.gameObject.CompareTag("Projectile") && healthSystem.GetHealth() > 0)
        {
            healthSystem.TakeDamage(enemyData.damageTaken);
            if (healthSystem.GetHealth() <= 0)
            {
                Die();
            }
        }
    }

    private void Update()
    {
        var distanceToTarget = target.position - transform.position;

        SetEnemyAction(enemyType, distanceToTarget);

        timeLeftToThrow -= Time.deltaTime;
    }
}
