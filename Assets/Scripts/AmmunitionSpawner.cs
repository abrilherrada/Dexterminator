using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ammunitionToSpawn = new GameObject[15];
    [SerializeField] List<Transform> spawnPositions;

    private Transform GetPosition()
    {
        if (spawnPositions.Count > 0)
        {
            var position = spawnPositions[0];
            spawnPositions.RemoveAt(0);
            return position;
        }
        return null;
    }

    private void InstantiateAllAmmunition()
    {
        foreach (GameObject ammunition in ammunitionToSpawn)
        {
            Transform positionTransform = GetPosition();
            Instantiate(ammunition, positionTransform);
        }
    }
    private void Awake()
    {
        InstantiateAllAmmunition();
    }
}
