
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    public static void SaveGame(GameObject player, GunPlaceHolderPlayer guns, HealthAndShield healthAndShield,int levelThis) {

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.rob";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(player, guns, healthAndShield,levelThis);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData loadPlayer() {
        string path = Application.persistentDataPath + "/playerdata.rob";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else {
            return null;
        }
    }
}
