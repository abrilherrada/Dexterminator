using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]
public struct SavedCharacterData
{
    public float health;
    public Vector3 initialPosition;

    public SavedCharacterData(float playerHealth, Vector3 playerInitialPosition)
    {
        health = playerHealth;
        initialPosition = playerInitialPosition;
    }
}

[Serializable]
public struct SavedUser
{
    public string username;
    public SavedCharacterData characterData;

    public SavedUser(string name, float currentHealth, Vector3 playerPosition)
    {
        username = name;
        characterData = new SavedCharacterData(currentHealth, playerPosition);
    }
}

[Serializable]
public struct SavedUserArray
{
    public List<SavedUser> users;
}

public class SavingManager : MonoBehaviour
{
    private string dataFilePath;
    private SavedUserArray savedUserArray = new SavedUserArray();

    private void Awake()
    {
        dataFilePath = Application.dataPath + "/data.json";
        LoadExistingData();
    }

    public void SaveData(string username, float currentHealth, Vector3 playerPosition)
    {
        int existingUserIndex = savedUserArray.users.FindIndex(user => user.username == username);

        if(existingUserIndex >= 0)
        {
            SavedUser existingUser = savedUserArray.users[existingUserIndex];
            
            existingUser.characterData.health = currentHealth;
            existingUser.characterData.initialPosition = playerPosition;

            savedUserArray.users[existingUserIndex] = existingUser;
        }
        else
        {
            SavedUser newUser = new SavedUser(username, currentHealth, playerPosition);

            savedUserArray.users.Add(newUser);
        }

        string jsonData = JsonUtility.ToJson(savedUserArray, true);

        File.WriteAllText(dataFilePath, jsonData);
    }
    
    public void LoadExistingData()
    {
        if(File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);

            savedUserArray = JsonUtility.FromJson<SavedUserArray>(jsonData);
        }
        else
        {
            savedUserArray.users = new List<SavedUser>();
        }
    }

    public SavedUser LoadData(string username)
    {
        SavedUser foundUser = savedUserArray.users.Find(user => user.username == username);
        
        return foundUser;
    }

    public SavedUser[] GetSavedUsers()
    {
        return savedUserArray.users.ToArray();
    }
}
