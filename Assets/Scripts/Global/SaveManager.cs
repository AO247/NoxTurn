using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SaveGameState(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file;
        file = File.Create(Application.persistentDataPath + "/saveData.dat");
        formatter.Serialize(file, saveData);
        file.Close();
    }

    public static SaveData LoadGameState()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            SaveData saveData = (SaveData)formatter.Deserialize(file);
            file.Close();
            return saveData;
        }
        else
        {
            Debug.LogError("Save file not found in " + Application.persistentDataPath + "/saveData.dat");
            return null;
        }
    }
}
