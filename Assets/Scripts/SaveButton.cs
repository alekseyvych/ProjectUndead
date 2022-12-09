using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    private Button saveButton;
    void Start()
    {
        saveButton = this.gameObject.GetComponent<Button>();
        saveButton.onClick.AddListener(SaveGame);
    }

    private void SaveGame()
    {
        Debug.Log("Saved");
        SaveSystem.SavePlaceableObjects(TownController.getPlaceableObjects());
    }
}
