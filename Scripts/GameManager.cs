using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerHealth = 100;
    public List<Item> collectedItems = new List<Item>();
    private string saveFilePath;
    private string encryptionKey = "myencryptionkey";
    public enum ItemType
    {
        Coin
    }
    [Serializable]
    public class Item
    {
        public ItemType type;
        public int value;

        public Item(ItemType type, int value)
        {
            this.type = type;
            this.value = value;
        }

        public void Collect()
        {
            Debug.Log("Item collected: " + type + " Value: " + value);
        }
    }
    public class GameData
    {
        public int playerHealth;
        public List<Item> items;

        public GameData(int health, List<Item> collectedItems)
        {
            playerHealth = health;
            items = collectedItems;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/gameData.json";
        LoadGameData();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Save"))
        {
            Debug.Log("Saved");
            GameData gameData = new GameData(playerHealth, collectedItems);
            SaveGameData(gameData);
        }
      
    }
    private void OnApplicationQuit()
    {
        GameData gameData = new GameData(playerHealth, collectedItems);
        SaveGameData(gameData);
    }
    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;

    }
    public void CollectItem(Item item)
    {
        collectedItems.Add(item);
        item.Collect();
    }
    public void LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            string encryptedData = File.ReadAllText(saveFilePath);
            string jsonData = XORDecrypt(encryptedData, encryptionKey);
            GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
            
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }
    public void SaveGameData(GameData gameData)
    {
        string jsonData = JsonUtility.ToJson(gameData);
        string encryptedData = XOREncrypt(jsonData, encryptionKey);
        File.WriteAllText(saveFilePath, encryptedData);
    }  
    private string XOREncrypt(string data, string key)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        int keyLength = keyBytes.Length;
        for (int ii = 0; ii < bytes.Length; ii++)
        {
            bytes[ii] = (byte)(bytes[ii] ^ keyBytes[ii % keyLength]);
        }
        return Convert.ToBase64String(bytes);
    }
    private string XORDecrypt(string encryptedData, string key)
    {
        byte[] bytes = Convert.FromBase64String(encryptedData);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        int keyLength = keyBytes.Length;
        for (int ii = 0; ii < bytes.Length; ii++)
        {
            bytes[ii] = (byte)(bytes[ii] ^ keyBytes[ii % keyLength]);
        }
        return Encoding.UTF8.GetString(bytes);
    }
}

