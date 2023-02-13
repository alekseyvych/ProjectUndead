using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "PlaceableObject")]
public class ObjectData : ScriptableObject
{
    public new string name;
    
    public string description;

    public GameManager.BuildingTypes buildingType;

    public GameManager.Buildings building;

    public Sprite image;

    public GameManager.Resources resourceType;
    
    public int maxLevel;
    
    public int[] buildingCost = new int[] {};

    public int[] buildingTime = new int[] {};

    public int[] buildingsPerHallLevel = new int[] { };

}
