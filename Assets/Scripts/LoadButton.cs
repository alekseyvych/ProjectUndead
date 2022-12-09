using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    private Button loadButton;
    void Start()
    {
        loadButton = this.gameObject.GetComponent<Button>();
        loadButton.onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        Debug.Log("Loaded");
        List<PlaceableObjectData> placeableObjectsData = SaveSystem.LoadPlaceableObjects();
        LoadSystem.LoadGame(placeableObjectsData);
    }
}
