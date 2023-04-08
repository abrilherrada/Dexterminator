using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private string characterName = "Dexter";
    private Vector3 characterSize = new Vector3(x: 1, y: 1, z: 1);
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector3 movementDirection = new Vector3(x: 0, y: 0, z: 1);
    private Vector3 CalculateDisplacement(float movSpeed, Vector3 movDirection)
    {
        var displacement = movSpeed * Time.deltaTime * movDirection;
        return displacement;
    }

    private void Move(float movSpeed, Vector3 movDirection, Transform movObject)
    {
        movObject.position += CalculateDisplacement(movSpeed, movDirection);
    }

    private void Awake()
    {
        var isSpeedValid = speed > 0;
        var isNameValid = !string.IsNullOrEmpty(characterName);

        Debug.Assert(isSpeedValid, "The speed is invalid");
        Debug.Assert(isNameValid, "The name is invalid");
    }

    void Start()
    {
        transform.localScale = characterSize;
    }

    // Update is called once per frame
    void Update()
    {
        Move(speed, movementDirection, transform);
    }
}
