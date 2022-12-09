using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Resource { ELIXIR, GOLD };

    public static Dictionary<string, (int,int[])> buildingTypesPlaced = new Dictionary<string, (int, int[])>()
    {
        {"CANNON", (0,new int[] {1,1,2,2})},
    };

    public static int townHall = 1;

    public static int GOLD_CAP = 500000;
    public static int ELIXIR_CAP = 500000;

    public static int GOLD = 500;
    public static int ELIXIR = 500;

    public Image oroImage;
    public Image elixirImage;

    public Text oroText;
    public Text elixirTest;

    public void updateResources(int oro, int elixir)
    {
        GOLD += oro;
        ELIXIR += elixir;

        updateImages(oro, elixir);
        updateTexts(oro, elixir);
    }

    private void updateImages(int oro, int elixir)
    {
        oroImage.GetComponent<RectTransform>().localScale = new Vector3(GOLD_CAP / GOLD, 1, 1);
        elixirImage.GetComponent<RectTransform>().localScale = new Vector3(ELIXIR_CAP / ELIXIR, 1, 1);
    }

    private void updateTexts(int oro, int elixir)
    {
        oroText.text = GOLD.ToString();
        elixirTest.text = ELIXIR.ToString();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
