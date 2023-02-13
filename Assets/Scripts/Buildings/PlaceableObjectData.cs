using System;
using UnityEngine;

[Serializable]
public class PlaceableObjectData : Data
{
    public int objectId;
    public float[] position;
    public GameManager.BuildingTypes buildingType;
    public GameManager.Buildings building;

    public int level;
    public PlaceableObjectData (PlaceableObject placeableObject)
    {
        position = new float[3];

        objectId = placeableObject.getObjectId();
        Vector3 placedPosition = placeableObject.getPlacedPosition();
        
        position[0] = placedPosition.x;
        position[1] = placedPosition.y;
        position[2] = placedPosition.z;

        buildingType = placeableObject.objectData.buildingType;
        building = placeableObject.objectData.building;

        level = placeableObject.level;
    }
}
