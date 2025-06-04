using System.Collections.Generic;
using System.IO;
using Inventory;
using UnityEngine;

namespace SaveSystem
{
    public class SaveController : MonoBehaviour
    {
        [SerializeField] InventorySystem inventorySystem;
        
        const string SaveFileName = "saveData.json";
        string _saveLocation;
        
        void Start()
        {
            _saveLocation = Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        public void SaveGame()
        {
            Dictionary<int, int> inventoryData = inventorySystem.SaveInventory();
            SaveData saveData = new SaveData()
            {
                InventoryData = inventoryData
            };
            
            File.WriteAllText(_saveLocation, JsonUtility.ToJson(saveData));
        }

        public void LoadSave()
        {
            if (!File.Exists(_saveLocation))
                return;
            
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
            inventorySystem.LoadInventory(saveData);
        }
    }
}
