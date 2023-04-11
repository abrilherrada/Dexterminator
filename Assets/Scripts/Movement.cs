using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    private string characterName = "Dexter";
    private Vector3 characterSize = new Vector3(x: 1, y: 1, z: 1);
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 720;

    private void Move(float movSpeed, float rotSpeed)
    {
        var direction = new Vector3(x: Input.GetAxis("Horizontal"), y: 0, z: Input.GetAxis("Vertical"));

        transform.Translate(movSpeed * Time.deltaTime * direction, Space.World);

        if (direction != Vector3.zero)
        {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
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
