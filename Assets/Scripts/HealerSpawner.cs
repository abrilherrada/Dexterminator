using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerSpawner : MonoBehaviour
{
    [SerializeField] HealingController[] healerToSpawn = new HealingController[8];
    [SerializeField] List<Transform> spawnPositions;

    private Transform GetPosition()
    {
        if(spawnPositions.Count > 0)
        {
            var position = spawnPositions[0];
            spawnPositions.RemoveAt(0);
            return position;
        }
        return null;
    }

    private void InstantiateAllHealers()
    {
        foreach (HealingController healer in healerToSpawn)
        {
            Transform positionTransform = GetPosition();
            Instantiate(healer, positionTransform);
        }
    }
    private void Awake()
    {
        InstantiateAllHealers();
    }
}
