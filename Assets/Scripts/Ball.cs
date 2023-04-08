using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 direction = new Vector3(x: 0, y: 0, z: 1f);
    [SerializeField] private int damage = 10;

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }
}
