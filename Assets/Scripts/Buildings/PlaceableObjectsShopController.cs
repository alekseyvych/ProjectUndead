using System;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void OpenPlaceableObjectDescriptionEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }

    public static void SpawnPlaceableObjectDescriptionEventListener<T, W, F>(this Button button, T param, W param2, F param3, Action<T, W, F> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param, param2, param3);
        });
    }
}


public class PlaceableObjectsShopController : MonoBehaviour
{
    public PlaceableObject[] placebleObjects;
    public Button exit;
    public GameObject antiClick;
    public GameObject shop;

    public Transform parent;

    public Button buttonTemplate;

    public Button shopButton;

    public TabGroup buttons;

    void Start()
    {
        Button instance;

        foreach (PlaceableObject placeableObject in placebleObjects)
        {
            ObjectData objectData = placeableObject.objectData;

            instance = Instantiate(buttonTemplate, transform);
            instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = objectData.name;
            instance.gameObject.transform.GetChild(1).GetComponent<Text>().text = objectData.buildingCost[0].ToString();
            instance.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = objectData.image;
            instance.gameObject.transform.GetChild(3).GetComponent<Text>().text = objectData.buildingTime[0].ToString();

            int buildingsPlaced = GameManager.buildingTypesPlaced[objectData.building];
            int maxBuildings = objectData.buildingsPerHallLevel[GameManager.townHall];
            
            instance.gameObject.transform.GetChild(5).GetComponent<Text>().text = buildingsPlaced.ToString() + "/" + maxBuildings .ToString();

            instance.gameObject.transform.GetChild(6).GetComponent<Button>().OpenPlaceableObjectDescriptionEventListener(placeableObject.objectData.description, DisplayInfo);

            instance.GetComponent<Button>().SpawnPlaceableObjectDescriptionEventListener(placeableObject, objectData.buildingCost[GameManager.townHall], objectData.resourceType, SpawnBuilding);

        }
    }

    void DisplayInfo(string placeableObjectDescription)
    {

    }

    void SpawnBuilding(PlaceableObject placeableObject, int price, GameManager.Resources resource)
    {
        long playerResource = 0;
        
        if (placeableObject.objectData.resourceType == GameManager.Resources.ELIXIR)
        {
            playerResource = GameManager.ELIXIR;
        }
        else
        {
            playerResource = GameManager.GOLD;
        }

        if (playerResource > price)
        {
            shop.SetActive(false);
            GlobalVariables.UI_OPEN = false;
            antiClick.SetActive(false);

            shopButton.image.color = Color.white;

            GameObject gameObject = Instantiate(placeableObject, Input.mousePosition, Quaternion.identity).gameObject;
            gameObject.GetComponent<MovePlaceableObjects>().isSelected = true;
            buttons.ResetAll();
        }

        else
        {
            Console.setText("You don't have enough money to buy this");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
