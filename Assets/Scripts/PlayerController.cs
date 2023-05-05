using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private KeyCode jumpKey = KeyCode.K;
    [SerializeField] private KeyCode spinKey = KeyCode.J;

    private string characterName = "Dexter";
    private Vector3 characterSize = new Vector3(x: 0.35f, y: 0.35f, z: 0.35f);
    [SerializeField] private float health = 100;
    private float maxHealth = 100;


    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 360;

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
        if (Input.GetKeyDown(jumpKey))
        {
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

    private void Awake()
    {
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
