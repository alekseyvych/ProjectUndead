﻿using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool EDIT_MODE;
    public static bool DELETE_MODE;
    public static bool INFO_MODE;
    public static bool SHOP;
    public static bool UI_OPEN;
    public static int MONEY = 10000;
    public static int MONTH_INCOMES = 0;
    public static int MONTH_EXPENSES = 0;
    public static bool BUILDING_MODE = false;
    public static bool WALL_MODE = false;


    void Awake()
    {
        EDIT_MODE = false;
        DELETE_MODE = false;
        INFO_MODE = true;
        UI_OPEN = false;
        SHOP = false;
        MONEY = 10000;
        BUILDING_MODE = false;
        WALL_MODE = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
