using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {

        if (!GlobalVariables.INFO_MODE)
        {
            GlobalVariables.INFO_MODE = true;
            button.image.color = Color.green;

            InfoPanelController.activatePanel();
        }
        else
        {
            GlobalVariables.INFO_MODE = false;
            button.image.color = Color.white;
        }
    }

    
}
