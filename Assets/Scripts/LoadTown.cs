using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTown : MonoBehaviour
{
    public GameObject prefab;

    private static GameObject loader;

    [SerializeField]
    private List<string> buildingsName= new List<string>();
    [SerializeField]
    private List<PlaceableObject> buildingsPrefab = new List<PlaceableObject>();

    
    private static Dictionary<string, PlaceableObject> buildingTypes = new Dictionary<string, PlaceableObject>()
    {
    };

    public void Awake()
    {
        for (int i = 0; i < buildingsName.Count; i++)
        {
            buildingTypes.Add(buildingsName[i], buildingsPrefab[i]);
        }
    }
    public void Start()
    {
        loader = this.gameObject;
    }
    public static void LoadTownData(List<PlaceableObjectData> placeableObjectsData)
    {
        foreach (Transform child in loader.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (PlaceableObjectData placeableObjectData in placeableObjectsData)
        {
            GameObject gameObject = Instantiate(buildingTypes[placeableObjectData.buildingType].gameObject, new Vector3(placeableObjectData.position[0], placeableObjectData.position[1], placeableObjectData.position[2]), Quaternion.identity);

            gameObject.GetComponent<PlaceableObject>().setObjectId(placeableObjectData.objectId);
            gameObject.GetComponent<MovePlaceableObjects>().isSelected = false;
    }
    }
}
