using System;
using UnityEngine;

[Serializable]
public class PlaceableObjectData : Data
{
    public int objectId;
    public float[] position;
    public string buildingType;
    public PlaceableObjectData (PlaceableObject placeableObject)
    {
        position = new float[3];

        objectId = placeableObject.getObjectId();
        Vector3 placedPosition = placeableObject.getPlacedPosition();
        
        position[0] = placedPosition.x;
        position[1] = placedPosition.y;
        position[2] = placedPosition.z;

        buildingType = placeableObject.getBuildingType();
    }
}
