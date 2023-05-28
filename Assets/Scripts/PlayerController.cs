using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : PC
{
    [SerializeField] private Rigidbody playerRigidbody;
    private KeyCode jumpKey = KeyCode.K;
    private KeyCode spinKey = KeyCode.J;

    private Vector3 characterSize = new Vector3(x: 0.35f, y: 0.35f, z: 0.35f);
    private Vector3 initialPosition = new Vector3(x: 0, y: 0, z: 0);

    private int collectedHealers = 0;
    private int collectedEnemies = 0;
    private int collectedAmmunition = 10;

    private float jumpingForce = 15f;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastMaxDistance = 0.1f;
    [SerializeField] private LayerMask raycastLayers;

    public void CollectHealer()
    {
        collectedHealers += 1;
    }

    public int GetCollectedHealers() => collectedHealers;

    public void CollectAmmunition(int amountGranted)
    {
        collectedAmmunition += amountGranted;
    }

    public void UseAmmunition(int amountUsed)
    {
        collectedAmmunition -= amountUsed;
    }

    public int GetCollectedAmmunition() => collectedAmmunition;

    public void CollectEnemy()
    {
        collectedEnemies += 1;
    }

    public int GetCollectedEnemies() => collectedEnemies;


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

    //Movement
    private void SetMovement()
    {
        var direction = new Vector3(x: Input.GetAxis("Horizontal"), y: 0, z: Input.GetAxis("Vertical"));
        if (direction != Vector3.zero)
        {
            Move(direction);
            Look(direction);

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

    public override void Die()
    {
        Debug.Log("Game over");
        //Set death animation
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
            GameManager.Instance.SaveData(health, initialPosition);
        }
    }

    private void Awake()
    {
        health = GameManager.Instance.GetSavedHealth();
    }

    void Start()
    {
        transform.localScale = characterSize;  
    }

    void Update()
    {
        SetMovement();
        Jump();
        Spin();
    }
}
