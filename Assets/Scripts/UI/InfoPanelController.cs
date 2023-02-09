using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    private static GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject.name);
        panel = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void activatePanel()
    {
        panel.gameObject.SetActive(true);
    }
    public static void deactivatePanel()
    {
        panel.gameObject.SetActive(false);
    }
}
