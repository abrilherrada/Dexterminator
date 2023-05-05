using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private Vector3 direction = new Vector3(x: 0, y: 0, z: 1f);
    [SerializeField] private float timeToDisappear = 3f;
    private float timeLeftToDisappear;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        timeLeftToDisappear = timeToDisappear;
    }

    void Update()
    {
        timeLeftToDisappear -= Time.deltaTime;
        if (timeLeftToDisappear <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(speed * Time.deltaTime * direction, Space.Self);
    }
}
