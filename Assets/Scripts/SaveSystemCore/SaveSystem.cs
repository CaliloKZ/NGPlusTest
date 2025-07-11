using System.IO;
using UnityEngine;

namespace SaveSystemCore
{
    public static class SaveSystem
    {
        public static void Save<T>(T data, string fileName)
        {
            string json = JsonUtility.ToJson(data);
            string path = $"{Application.persistentDataPath}/{fileName}";
            File.WriteAllText(path, json);
        }

        public static T Load<T>(string fileName)
        {
            string path = $"{Application.persistentDataPath}/{fileName}";
            if (!File.Exists(path))
            {
                Debug.LogError($"File {fileName} does not exist");
                return default;
            }
            
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        }

        public static void Delete(string fileName)
        {
            string path = $"{Application.persistentDataPath}/{fileName}";
            if (!File.Exists(path))
            {
                Debug.LogError($"File {fileName} does not exist");
                return;
            }
            
            File.Delete(path);
            Debug.Log($"File {fileName} deleted");
        }
    }
}
