using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableObject : MonoBehaviour
{

    private int objectId = -1;
    private Vector3 placedPosition = new Vector3();

    public ObjectData objectData;

    public int level;

    public int getObjectId()
    {
        return this.objectId;
    }

    public void setObjectId(int objectId)
    {
        this.objectId = objectId;
    }

    public Vector3 getPlacedPosition()
    {
        return this.placedPosition;
    }


    public void setPlacedPosition(Vector3 placedPosition)
    {
        this.placedPosition = placedPosition;
    }
}
