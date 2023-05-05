using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum EnemyTypes
{
    Spider1,
    Spider2
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private Transform target;
    [SerializeField] private Rock rockToThrow;
    [SerializeField] private Transform throwingPoint;
    [SerializeField] private float timeToThrow = 2f;
    private float timeLeftToThrow;


    [SerializeField] private float distanceToStartChasing = 5f;
    [SerializeField] private float distanceToStopChasing = 0;
    [SerializeField] private float distanceToStartThrowing = 4f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float health = 100f;

    private void SetEnemyAction(EnemyTypes enemyType, Vector3 direction)
    {
        switch(enemyType)
        {
            case EnemyTypes.Spider1:
                Chase(direction);
                break;
            case EnemyTypes.Spider2:
                Look(direction.normalized);
                Throw(direction);
                break;
            default:
                break;
        }
    }

    private float SetDamageTaken(EnemyTypes enemyType)
    {
        switch (enemyType) 
        {
            case EnemyTypes.Spider1:
                return 25f;
            case EnemyTypes.Spider2:
                return 20f;
            default:
                break;
        }
        return 0;
    }

    private float SetDamageDone(EnemyTypes enemyType) 
    {
        switch (enemyType)
        {
            case EnemyTypes.Spider1:
                return 10f;
            case EnemyTypes.Spider2:
                return 15f;
            default:
                break;
        }
        return 0;
    }

    private void Look(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Move(float movSpeed, Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.position += movSpeed * Time.deltaTime * direction;

            animator.SetBool("isMoving", true);
        }
    }

    private void Chase(Vector3 direction)
    {
        Look(direction.normalized);

        if (distanceToStartChasing >= direction.magnitude && direction.magnitude > distanceToStopChasing && health > 0)
        {
            Move(movementSpeed, direction.normalized);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void Throw(Vector3 direction)
    {
        if (distanceToStartThrowing >= direction.magnitude && timeLeftToThrow <= 0)
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

    private void TakeDamage(float damagePoints)
    {
        health -= damagePoints;
        animator.SetTrigger("isTakingDamage");

        if (health - damagePoints <= 0)
        {
            health = 0;
            animator.SetBool("isThrowing", false);
            animator.SetInteger("health", 0);
            StartCoroutine(WaitForDeathAnimation());
        }
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out PlayerController player))
        {
            animator.SetTrigger("isAttacking");
            player.TakeDamage(SetDamageDone(enemyType));  
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(SetDamageTaken(enemyType));
        }
    }

    private void Update()
    {
        var distanceToTarget = target.position - transform.position;

        SetEnemyAction(enemyType, distanceToTarget);

        timeLeftToThrow -= Time.deltaTime;
    }
}
