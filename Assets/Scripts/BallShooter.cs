using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private KeyCode leftShootKey = KeyCode.J;
    [SerializeField] private KeyCode doubleShootKey = KeyCode.K;
    [SerializeField] private KeyCode rightShootKey = KeyCode.L;

    [SerializeField] private Ball ballToShoot;
    [SerializeField] private Transform shootingPoint1;
    [SerializeField] private Transform shootingPoint2;

    [SerializeField] private float timeToShoot = 1f;
    private float timeLeftToShoot;

    private void Shoot(Transform shootingPoint)
    {
        Instantiate(ballToShoot, shootingPoint.position, shootingPoint.rotation);
        timeLeftToShoot = timeToShoot;
    }
    private void Inputs()
    {
        if (timeLeftToShoot <= 0)
        {
            if (Input.GetKeyDown(leftShootKey))
            {
                Shoot(shootingPoint1);
            }
            if (Input.GetKeyDown(doubleShootKey))
            {
                Shoot(shootingPoint1);
                Shoot(shootingPoint2);
            }
            if (Input.GetKeyDown(rightShootKey))
            {
                Shoot(shootingPoint2);
            }
        }
    }

    private void Update()
    {
        timeLeftToShoot -= Time.deltaTime;
        Inputs();
    }
}
