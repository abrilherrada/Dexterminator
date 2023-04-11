using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 size = new Vector3(x: 0.1f, y: 0.1f, z: 0.1f);
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 direction = new Vector3(x: 0, y: 0, z: 1f);
    [SerializeField] private int damage = 10;
    [SerializeField] private float timeToDisappear = 3f;
    private float timeLeftToDisappear;

    private void DuplicateSize()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            size *= 2;
            transform.localScale = size;
        }
    }

    private void Awake()
    {
        timeLeftToDisappear = timeToDisappear;
        transform.localScale = size;
    }

    void Update()
    {
        timeLeftToDisappear -= Time.deltaTime;
        if (timeLeftToDisappear <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(speed * Time.deltaTime * direction, Space.Self);
        DuplicateSize();
    }
}
