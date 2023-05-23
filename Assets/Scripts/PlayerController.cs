using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator animator;
    private KeyCode jumpKey = KeyCode.K;
    private KeyCode spinKey = KeyCode.J;

    private string characterName = "Dexter";
    private Vector3 characterSize = new Vector3(x: 0.35f, y: 0.35f, z: 0.35f);
    private Vector3 initialPosition = new Vector3(x: 0, y: 0, z: 0);
    private float health;
    private float maxHealth = 100;

    private int collectedHealers = 0;
    private int collectedEnemies = 0;
    private int collectedAmmunition = 10;

    private float jumpingForce = 15f;
    private float movementSpeed = 2f;
    private float rotationSpeed = 360;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastMaxDistance = 0.1f;
    [SerializeField] private LayerMask raycastLayers;

    public float GetHealth()
    {
        return health;
    }

    public void CollectHealer()
    {
        collectedHealers += 1;
    }

    public int GetCollectedHealers()
    {
        return collectedHealers;
    }

    public void CollectAmmunition(int amountGranted)
    {
        collectedAmmunition += amountGranted;
    }

    public void UseAmmunition(int amountUsed)
    {
        collectedAmmunition -= amountUsed;
    }

    public int GetCollectedAmmunition()
    {
        return collectedAmmunition;
    }

    public void CollectEnemy()
    {
        collectedEnemies += 1;
    }

    public int GetCollectedEnemies()
    {
        return collectedEnemies;
    }

    private bool IsOnGround()
    {
        bool isHitting = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, raycastMaxDistance, raycastLayers);

        if (isHitting)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Borrar:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + raycastOrigin.forward * raycastMaxDistance);
    }

    //Movement
    private void Move(float movSpeed, float rotSpeed)
    {
        var direction = new Vector3(x: Input.GetAxis("Horizontal"), y: 0, z: Input.GetAxis("Vertical"));

        if (direction != Vector3.zero)
        {
            transform.Translate(movSpeed * Time.deltaTime * direction, Space.World);

            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotSpeed * Time.deltaTime);
            
            animator.SetBool("isMoving", true);

            if (direction.x != 0)
            {
                animator.SetBool("isRotating", true);
            }
            else
            {
                animator.SetBool("isRotating", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    //Abilities
    private void Jump()
    {
        if (Input.GetKeyDown(jumpKey) && IsOnGround())
        {
            Vector3 forceVector = transform.up * jumpingForce;
            playerRigidbody.AddForce(forceVector, ForceMode.Impulse);
            animator.SetTrigger("isJumping");
        }
    }

    private void Spin()
    {
        if (Input.GetKeyDown(spinKey))
        {
            animator.SetTrigger("isSpinning");
        }
    }

    //Health
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

        if (health - damagePoints <= 0)
        {
            health = 0;
            animator.SetInteger("health", 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            TakeDamage(10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            //GameManager.Instance.SaveData(health, initialPosition);
        }
    }

    private void Awake()
    {
        health = maxHealth;
        var isSpeedValid = movementSpeed > 0;
        var isNameValid = !string.IsNullOrEmpty(characterName);

        Debug.Assert(isSpeedValid, "The speed is invalid");
        Debug.Assert(isNameValid, "The name is invalid");
    }

    void Start()
    {
        transform.localScale = characterSize;
    }

    void Update()
    {
        Move(movementSpeed, rotationSpeed);
        Jump();
        Spin();
    }
}
