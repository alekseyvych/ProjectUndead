using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class LoadSystem
{

    public static void LoadGame(List<PlaceableObjectData> placeableObjectsData)
    {
        LoadTown.LoadTownData(placeableObjectsData);
    }
}
