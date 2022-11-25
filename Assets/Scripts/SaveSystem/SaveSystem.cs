using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
   public static void SavePlaceableObject (DragBuildings building) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/placeableObject.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlaceableObjectData data = new PlaceableObjectData(building);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlaceableObjectData LoadPlaceableObject()
    {
        string path = Application.persistentDataPath + "/placeableObject.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlaceableObjectData data = formatter.Deserialize(stream) as PlaceableObjectData;
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
