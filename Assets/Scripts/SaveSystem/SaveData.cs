using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class SaveData 
{
    public static int IdCount;

    public Dictionary<string, PlaceableObjectData> placeableObjectsData = new Dictionary<string, PlaceableObjectData>();

    public static string GenerateId()
    {
        IdCount++;
        return IdCount.ToString();
    }

    public void AddData(Data data)
    {
        if (data is PlaceableObjectData plObjData)
        {
            if (placeableObjectsData.ContainsKey(plObjData.ID))
            {
                placeableObjectsData[plObjData.ID] = plObjData;
            }
            else
            {
                placeableObjectsData.Add(plObjData.ID, plObjData);
            }
        }
    }

    public void RemoveData(Data data)
    {
        if (data is PlaceableObjectData plObjData)
        {
            if (placeableObjectsData.ContainsKey(plObjData.ID))
            {
                placeableObjectsData.Remove(plObjData.ID);
            }
        }
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        placeableObjectsData ??= new Dictionary<string, PlaceableObjectData>();
    }
}
