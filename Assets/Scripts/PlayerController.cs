using System;
using System.Collections;
using UnityEngine;

public class PlayerController : PC
{
    public static PlayerController player;

    [SerializeField] private PostProcessingController postProcessingController;

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Renderer playerRenderer;

    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode spinKey = KeyCode.Mouse1;

    private Vector3 characterSize = new Vector3(x: 0.35f, y: 0.35f, z: 0.35f);
    private Vector3 initialPosition = new Vector3(x: 0, y: 0, z: 0);

    private int collectedHealers = 0;
    private int collectedEnemies = 0;
    private int collectedAmmunition = 10;

    private bool isSpinning = false;

    private float immunityTime = 2f;
    private float immunityTimeLeft;

    private float flickeringTime = 0.1f;
    private float flickeringTimeLeft;

    private float jumpingForce = 20f;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastMaxDistance = 0.1f;
    [SerializeField] private LayerMask raycastLayers;

    public event Action OnItemCollected;

    public void CollectHealer()
    {
        collectedHealers += 1;
        OnItemCollected?.Invoke();
    }

    public int GetCollectedHealers() => collectedHealers;

    public void CollectAmmunition(int amountGranted)
    {
        collectedAmmunition += amountGranted;
        OnItemCollected?.Invoke();
    }

    public void UseAmmunition(int amountUsed)
    {
        collectedAmmunition -= amountUsed;
        OnItemCollected?.Invoke();
    }

    public int GetCollectedAmmunition() => collectedAmmunition;

    public void CollectEnemy()
    {
        collectedEnemies += 1;
        OnItemCollected?.Invoke();
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
    private void SetMovement(Vector3 direction)
    {
        Rotate(GetRotation());

        if (direction != Vector3.zero)
        {
            Walk(direction);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private Vector2 GetRotation()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        return new Vector2(mouseX, mouseY);
    }

    private Vector3 GetMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        return new Vector3(horizontal, 0, vertical).normalized;
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
            isSpinning = true;
            animator.SetTrigger("isSpinning");
            StartCoroutine(WaitForSpinAnimation());
        }
    }

    public bool GetIsSpinning() => isSpinning;

    private IEnumerator WaitForSpinAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        isSpinning = false;
    }

    public void CanTakeDamage(float damagePoints)
    {
        if(immunityTimeLeft <= 0)
        {
            healthSystem.TakeDamage(damagePoints);
            if(healthSystem.GetHealth() > 0)
            {
                immunityTimeLeft = immunityTime;
                playerRenderer.enabled = false;
                flickeringTimeLeft = flickeringTime;
            }
        }
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Game over"); //Replace with Game Over screen
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            CanTakeDamage(10f);
            postProcessingController.UseDamagedEffect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            GameManager.Instance.SaveData(healthSystem.GetHealth(), initialPosition);
        }
    }

    void Start()
    {
        healthSystem.SetHealth(GameManager.Instance.GetSavedHealth());
        transform.localScale = characterSize;  
    }

    void Update()
    {
        SetMovement(GetMovement());
        Jump();
        Spin();

        if (immunityTimeLeft > 0)
        {
            immunityTimeLeft -= Time.deltaTime;
            flickeringTimeLeft -= Time.deltaTime;

            if(flickeringTimeLeft <= 0 )
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flickeringTimeLeft = flickeringTime;
            }
            if(immunityTimeLeft <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }
}
