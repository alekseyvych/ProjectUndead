using System;
using UnityEngine;

[Serializable]
public class PlaceableObjectData : Data
{
    public float[] position;

    public PlaceableObjectData (DragBuildings building)
    {
        position = new float[3];
        position[0] = building.transform.position.x;
        position[1] = building.transform.position.y;
        position[2] = building.transform.position.z;
    }
}
