using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Spider1,
    Spider2
}

public struct EnemyData
{
    public float damageTaken;
    public float damageDone;
    public int pointsForKill;
}

public class EnemyController : PC
{
    private Dictionary<string, EnemyData> enemiesDictionary = new Dictionary<string, EnemyData>();
    private string enemyKey;

    [SerializeField] private PlayerController player;
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
    [SerializeField] private float distanceToStopChasing = 0;
    [SerializeField] private float distanceToStartThrowing = 4f;

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

        if (distanceToStartChasing >= direction.magnitude && direction.magnitude > distanceToStopChasing && health > 0 && IsTargetInSight() && direction != Vector3.zero)
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
        GameManager.Instance.AddScore(GetEnemy().pointsForKill);
        player.CollectEnemy();
        StartCoroutine(WaitForDeathAnimation());
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
            player.TakeDamage(GetEnemy().damageDone);
        }

        if (collision.gameObject.CompareTag("Projectile") && health > 0)
        {
            TakeDamage(GetEnemy().damageTaken);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private EnemyData GetEnemy()
    {
        if(enemiesDictionary.Count > 0)
        {
            var enemy = enemiesDictionary[enemyKey];
            return enemy; 
        }
        return default;
    }

    private void Awake()
    {
        if (enemyType == EnemyTypes.Spider1)
        {
            EnemyData enemyData = new EnemyData()
            {
                damageTaken = 25f,
                damageDone = 10f,
                pointsForKill = 10,
            };
            enemiesDictionary.Add("Spider1", enemyData);
            enemyKey = "Spider1";
        }
        if(enemyType == EnemyTypes.Spider2)
        {
            EnemyData enemyData = new EnemyData()
            {
                damageTaken = 20f,
                damageDone = 15f,
                pointsForKill = 20,
            };
            enemiesDictionary.Add("Spider2", enemyData);
            enemyKey = "Spider2";
        }
    }

    private void Update()
    {
        var distanceToTarget = target.position - transform.position;

        SetEnemyAction(enemyType, distanceToTarget);

        timeLeftToThrow -= Time.deltaTime;
    }
}
