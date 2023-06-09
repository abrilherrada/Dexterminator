using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionController : MonoBehaviour
{
    [SerializeField] GameObject ammunition;

    private int ammunitionAmount = 4;
    private int pointsForCollecting = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out PlayerController player))
        {
            GameManager.Instance.AddScore(pointsForCollecting);
            player.CollectAmmunition(ammunitionAmount);
            Destroy(ammunition);
        }
    }
}
