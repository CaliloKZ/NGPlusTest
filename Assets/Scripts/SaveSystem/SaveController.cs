using System;
using System.Collections.Generic;
using System.IO;
using Inventory;
using NUnit.Framework;
using UnityEngine;

namespace SaveSystem
{
    public class SaveController : MonoBehaviour
    {
        static SaveController _instance;
        [SerializeField] InventorySystem inventorySystem;
        
        const string SaveFileName = "saveData.json";
        string _saveLocation;


        void Awake()
        {
            if(null != _instance)
                Destroy(_instance);
            
            _instance = this;
        }
        
        void Start()
        {
            _saveLocation = Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        public static void StartSaveGame()
        {
            _instance.SaveGame();
        }
        
        public static void StartLoadGame()
        {
            _instance.LoadSave();
        }

        void SaveGame()
        {
            List<InventorySaveData> inventoryData = inventorySystem.SaveInventory();
            SaveData saveData = new SaveData()
            {
                inventorySaveData = inventoryData
            };

            string jsonSave = JsonUtility.ToJson(saveData);
            File.WriteAllText(_saveLocation, JsonUtility.ToJson(saveData));
        }

        void LoadSave()
        {
            if (!File.Exists(_saveLocation))
                return;
            
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
            inventorySystem.LoadInventory(saveData);
        }
    }
}
