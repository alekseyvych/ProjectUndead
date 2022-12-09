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
            instance = Instantiate(buttonTemplate, transform);
            instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = placeableObject.name;
            instance.gameObject.transform.GetChild(1).GetComponent<Text>().text = placeableObject.buildingCost[0].ToString();
            instance.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = placeableObject.image;
            instance.gameObject.transform.GetChild(3).GetComponent<Text>().text = placeableObject.buildingTime[0].ToString();
            (int, int[]) tuple = GameManager.buildingTypesPlaced[placeableObject.getBuildingType()];
            instance.gameObject.transform.GetChild(5).GetComponent<Text>().text = tuple.Item1.ToString() + "/" + tuple.Item2[GameManager.townHall].ToString();

            instance.gameObject.transform.GetChild(6).GetComponent<Button>().OpenPlaceableObjectDescriptionEventListener(placeableObject.description, DisplayInfo);

            instance.GetComponent<Button>().SpawnPlaceableObjectDescriptionEventListener(placeableObject, placeableObject.buildingCost[GameManager.townHall], placeableObject.resourceType, SpawnBuilding);

        }
    }

    void DisplayInfo(string placeableObjectDescription)
    {

    }

    void SpawnBuilding(PlaceableObject placeableObject, int price, GameManager.Resource resource)
    {
        long playerResource = 0;
        
        if (placeableObject.resourceType == GameManager.Resource.ELIXIR)
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
