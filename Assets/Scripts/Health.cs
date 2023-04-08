using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private int Heal(int playerHealth)
    {
        if (playerHealth <= 90 && playerHealth > 0)
        {
            return playerHealth += 10;
        }
        else if (playerHealth >= 91)
        {
            return playerHealth += 100 - playerHealth;
        }
        return playerHealth;
    }

    private int Damage(int playerHealth)
    {
        if (playerHealth >= 5)
        {
            return playerHealth -= 5;
        }
        else if (playerHealth < 5 && playerHealth > 0)
        {
            return playerHealth -= playerHealth;
        }
        return playerHealth;
    }

    void Start()
    {
        health = Heal(health);
        Debug.Log(health);
        health = Damage(health);
        Debug.Log(health);
    }

    void Update()
    {
        
    }
}
