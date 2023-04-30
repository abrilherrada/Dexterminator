using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private KeyCode shootKey = KeyCode.L;

    [SerializeField] private Ball ballToShoot;
    [SerializeField] private Transform shootingPoint1;
    [SerializeField] private Transform shootingPoint2;

    [SerializeField] private float timeToShoot = 0.5f;
    private float timeLeftToShoot;

    private void Shoot(Transform shootingPoint)
    {
        Instantiate(ballToShoot, shootingPoint.position, shootingPoint.rotation);
        timeLeftToShoot = timeToShoot;
    }
    private void Inputs()
    {
        if (timeLeftToShoot <= 0 && Input.GetKeyDown(shootKey))
        {
            Shoot(shootingPoint1);
            Shoot(shootingPoint2);
        }
    }

    private void Update()
    {
        timeLeftToShoot -= Time.deltaTime;
        Inputs();
    }
}
