using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingController : MonoBehaviour
{
    [SerializeField] Consumer consumer;
    [SerializeField] private float healingPoints = 10;
    private float lastChange;
    private float interval = 0.5f;
    private int pointsForCollecting = 5;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out PlayerController player) && Time.time - lastChange > interval)
        {
            consumer.Consume();
            lastChange = Time.time;
          
            if (consumer.allConsumed)
            {
                GameManager.Instance.AddScore(pointsForCollecting);
                player.GetHealed(healingPoints);
                healingPoints = 0;
                pointsForCollecting = 0;
            }
        }
    }
}
