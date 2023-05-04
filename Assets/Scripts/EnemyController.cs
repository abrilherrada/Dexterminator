using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Ant,
    Spider1,
    Spider2
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToStop = 2;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float rotationSpeed = 60;
    [SerializeField] private float health = 100;

    private void SetEnemyAction(EnemyTypes enemyType, Vector3 direction)
    {
        switch(enemyType)
        {
            case EnemyTypes.Ant:
                Look(direction.normalized);
                break;
            case EnemyTypes.Spider1:
                Chase(direction);
                break;
            default:
                break;
        }
    }

    private float SetDamageTaken(EnemyTypes enemyType)
    {
        switch (enemyType) 
        {
            case EnemyTypes.Ant:
                return 34;
            case EnemyTypes.Spider1:
                return 50;
        }
        return 0;
    }

    private void Look(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Move(Vector3 direction)
    {
        transform.position += movementSpeed * Time.deltaTime * direction;
    }

    private void Chase(Vector3 direction)
    {
        Look(direction.normalized);

        if (distanceToStop < direction.magnitude && direction.magnitude > 0.1f)
        {
            Move(direction.normalized);
        }
    }

    private void TakeDamage(float damagePoints)
    {
        if (health - damagePoints <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            health -= damagePoints;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(SetDamageTaken(enemyType));
            Debug.Log(health);
        }
    }

    private void Awake()
    {
        if (gameObject.name == "Ant")
        {
            enemyType = EnemyTypes.Ant;
        }
        else if (gameObject.name == "Spider 1")
        {
            enemyType = EnemyTypes.Spider1;
        }
    }

    private void Update()
    {
        var distanceToTarget = target.position - transform.position;

        SetEnemyAction(enemyType, distanceToTarget);
    }
}
