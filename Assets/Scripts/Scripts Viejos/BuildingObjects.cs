using UnityEngine;

[CreateAssetMenu(fileName = "BuildingObjects", menuName = "ScriptableObjects/BuildingObjects")]
public class BuildingObjects : ScriptableObject
{
    public string buildingName;
    public Sprite image;
    public string description;
    public int price;
    public GameObject prefab;

}
