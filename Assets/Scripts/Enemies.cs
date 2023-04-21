using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Lookout,
    Chaser
}

public class Enemies : MonoBehaviour
{
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToStop = 2;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float rotationSpeed = 60;

    private void SetEnemyAction(EnemyTypes enemyType, Vector3 direction)
    {
        switch(enemyType)
        {
            case EnemyTypes.Lookout:
                Look(direction.normalized);
                break;
            case EnemyTypes.Chaser:
                Chase(direction);
                break;
            default:
                break;
        }
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

    private void Awake()
    {
        if (gameObject.name == "Enemy 1")
        {
            enemyType = EnemyTypes.Lookout;
        }
        else if (gameObject.name == "Enemy 2")
        {
            enemyType = EnemyTypes.Chaser;
        }
    }

    private void Update()
    {
        var distanceToTarget = target.position - transform.position;

        SetEnemyAction(enemyType, distanceToTarget);
    }
}
