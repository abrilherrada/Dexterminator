using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private Image shooterCooldown;

    //[SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private KeyCode shootKey = KeyCode.L;

    [SerializeField] private Ball ballToShoot;
    [SerializeField] private Transform shootingPoint1;
    [SerializeField] private Transform shootingPoint2;

    //[SerializeField] private Vector3 shootingDirection = new Vector3(x: 0, y: 0, z: 1f);
    //[SerializeField] private float shootingForce;
    [SerializeField] private float timeToShoot = 0.5f;
    private float timeLeftToShoot;

    private void Shoot(Transform shootingPoint)
    {
        if(player.GetCollectedAmmunition() > 0)
        {
            Instantiate(ballToShoot, shootingPoint.position, shootingPoint.rotation);

            player.UseAmmunition(1);

            //Vector3 forceVector = shootingDirection * shootingForce;
            //ballRigidbody.AddForce(forceVector, ForceMode.Impulse);

            timeLeftToShoot = timeToShoot;
            shooterCooldown.fillAmount = 0;
        }
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
        shooterCooldown.fillAmount += Time.deltaTime * 2;
        Inputs();
    }
}
