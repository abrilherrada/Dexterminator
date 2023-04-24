using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : MonoBehaviour
{
    [SerializeField] private float healingPoints = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
                player.GetHealed(healingPoints);
                healingPoints = 0;
        }
    }
}
