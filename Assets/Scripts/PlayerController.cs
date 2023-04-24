using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private string characterName = "Dexter";
    private Vector3 characterSize = new Vector3(x: 1, y: 1, z: 1);
    [SerializeField] private float health = 100;
    private float maxHealth = 100;


    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 360;

    //Movement
    private void Move(float movSpeed, float rotSpeed)
    {
        var direction = new Vector3(x: Input.GetAxis("Horizontal"), y: 0, z: Input.GetAxis("Vertical"));

        transform.Translate(movSpeed * Time.deltaTime * direction, Space.World);

        if (direction != Vector3.zero)
        {
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotSpeed * Time.deltaTime);
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
        if (health - damagePoints < 0)
        {
            health = 0;
        }
        else
        {
            health -= damagePoints;
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
    }
}
