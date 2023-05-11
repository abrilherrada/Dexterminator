using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]
public struct SavedData
{
    public float health;
    public Vector3 initialPosition;

    public SavedData(float playerHealth, Vector3 playerInitialPosition)
    {
        health = playerHealth;
        initialPosition = playerInitialPosition;
    }
}

public class SavingManager : MonoBehaviour
{
    public void Save(float currentHealth, Vector3 playerPosition)
    {
        var data = new SavedData(currentHealth, playerPosition);
        string jsonData = JsonUtility.ToJson(data, true);
        var path = Application.dataPath + "/data.json";
        File.WriteAllText(path, jsonData);

        Debug.Log($"la vida guardada es {currentHealth}");
        Debug.Log($"la posicion guardada es {playerPosition}");
    }

    public void Load(float currentHealth, Vector3 playerPosition)
    {
        var path = Application.dataPath + "/data.json";
        var jsonData = File.ReadAllText(path);
        var loadedJson = JsonUtility.FromJson<SavedData>(jsonData);

        currentHealth = loadedJson.health;
        playerPosition = loadedJson.initialPosition;

        Debug.Log($"la vida cargada es {currentHealth}");
        Debug.Log($"la posicion cargada es {playerPosition}");
    }
}
