using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableObject : MonoBehaviour
{

    private int objectId = -1;
    private Vector3 placedPosition = new Vector3();
    public string name;
    private string buildingType = "CANNON";
    public Sprite image;
    public GameManager.Resource resourceType = GameManager.Resource.GOLD;
    public int level = 1;
    public int maxLevel = 4;
    public int[] buildingCost = new int[] { 10, 20, 30, 40};
    public int[] buildingTime = new int[] { 10, 20, 30, 40 };
    public Material[] materials = new Material[4];
    public string description = "description";

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

    public string getBuildingType()
    {
        return this.buildingType;
    }

    public void setPlacedPosition(Vector3 placedPosition)
    {
        this.placedPosition = placedPosition;
    }
}
