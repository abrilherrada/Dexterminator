using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]
public struct SavedCharacterData
{
    public float health;
    public int ammunition;
    public int score;

    public SavedCharacterData(float playerHealth, int playerAmmunition, int playerScore)
    {
        health = playerHealth;
        ammunition = playerAmmunition;
        score = playerScore;
    }
}

[Serializable]
public struct SavedUser
{
    public string username;
    public SavedCharacterData characterData;

    public SavedUser(string name, float currentHealth, int currentAmmunition, int currentScore)
    {
        username = name;
        characterData = new SavedCharacterData(currentHealth, currentAmmunition, currentScore);
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

    public void SaveData(string username, float currentHealth, int currentAmmunition, int currentScore)
    {
        int existingUserIndex = savedUserArray.users.FindIndex(user => user.username == username);

        if(existingUserIndex >= 0)
        {
            SavedUser existingUser = savedUserArray.users[existingUserIndex];
            
            existingUser.characterData.health = currentHealth;
            existingUser.characterData.ammunition = currentAmmunition;
            existingUser.characterData.score = currentScore;

            savedUserArray.users[existingUserIndex] = existingUser;
        }
        else
        {
            SavedUser newUser = new SavedUser(username, currentHealth, currentAmmunition, currentScore);
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

    public SavedCharacterData LoadData(string username)
    {
        SavedUser foundUser = savedUserArray.users.Find(user => user.username == username);
        return foundUser.characterData;
    }

    public SavedUser[] GetSavedUsers()
    {
        return savedUserArray.users.ToArray();
    }
}
