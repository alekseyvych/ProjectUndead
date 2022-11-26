using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTown : MonoBehaviour
{
    public GameObject prefab;

    private static GameObject loader;
    private static GameObject staticPrefab;

    public void Start()
    {
        loader = this.gameObject;
        staticPrefab = prefab;

    }
    public static void LoadTownData(List<PlaceableObjectData> placeableObjectsData)
    {
        foreach (Transform child in loader.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (PlaceableObjectData placeableObjectData in placeableObjectsData)
        {
            GameObject gameObject = Instantiate(staticPrefab, new Vector3(placeableObjectData.position[0], placeableObjectData.position[1], placeableObjectData.position[2]), Quaternion.identity);

            gameObject.GetComponent<PlaceableObject>().setObjectId(placeableObjectData.objectId);
            gameObject.GetComponent<MovePlaceableObjects>().isSelected = false;
    }
    }
}
