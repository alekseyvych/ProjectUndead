using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
   public static void SavePlaceableObjects (List<PlaceableObject> placeableObjects) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/placeableObject.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        List<PlaceableObjectData> placeableObjectsData = new List<PlaceableObjectData>();
        foreach (PlaceableObject placeableObject in placeableObjects)
        {
            placeableObjectsData.Add(new PlaceableObjectData(placeableObject));
            
        }

        formatter.Serialize(stream, placeableObjectsData);
        stream.Close();
    }

    public static List<PlaceableObjectData> LoadPlaceableObjects()
    {
        string path = Application.persistentDataPath + "/placeableObject.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<PlaceableObjectData> data = formatter.Deserialize(stream) as List<PlaceableObjectData>;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }
}
