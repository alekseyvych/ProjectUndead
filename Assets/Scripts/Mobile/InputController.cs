using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Vector3 touchStart;
   
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    
    public float maxX = 20;
    public float minX = -20;
    
    public float maxZ = 20;
    public float minZ = -20;

    private float holdDuration = 0f;
    private bool isHolding = false;
    private bool held = false;
    private bool moved = false;
    private float holdDurationThreshold = 1f;

    private Vector3 startPos;
    private Vector3 endPos;

    private GameObject selection = null;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                startPos = Input.GetTouch(0).position;
                Debug.Log(touchStart);
                if (Physics.Raycast(touchStart, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.transform.gameObject.CompareTag("Building"))
                {
                    selection = hit.transform.gameObject;
                    Debug.Log(hit.transform.gameObject.name + "  " + hit.transform.gameObject.tag);
                }

                isHolding = true;
                holdDuration = 0f;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved && selection==null)
            {
                Vector3 currentPos = Input.GetTouch(0).position;
                float dist = Vector3.Distance(startPos, currentPos);
                if (dist > 12)
                {
                    isHolding = false;
                    Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    Vector3 newPosition = new Vector3(0, Camera.main.transform.position.y, 0);
                    newPosition.x = Mathf.Clamp(Camera.main.transform.position.x + direction.x, minX, maxX);
                    newPosition.z = Mathf.Clamp(Camera.main.transform.position.z + direction.z, minZ, maxZ);

                    Camera.main.transform.position = newPosition;

                    moved = true;
                }
                else
                {
                    if (Physics.Raycast(touchStart, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.transform.gameObject.CompareTag("Building"))
                    {
                        if (hit.transform.gameObject != selection)
                        {
                            isHolding = false;
                            holdDuration = 0f;
                            selection = hit.transform.gameObject;
                        }
                    }
                }
            }

            if (isHolding)
            {
                holdDuration += Time.deltaTime;

                if (holdDuration >= holdDurationThreshold)
                {
                    held = true;

                    if (selection != null)
                    {
                        selection.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        selection.GetComponent<MovePlaceableObjects>().selected();
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) //Quitar canceled
            {
                isHolding = false;

                if (!held && !moved)
                {
                    if (Physics.Raycast(touchStart, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.transform.gameObject.CompareTag("Building"))
                    {
                        if (hit.transform.gameObject == selection)
                        {
                            hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        }
                    }
                }
                if (held)
                {
                    held = false;

                    if (selection !=null)
                    {
                        selection.GetComponent<MovePlaceableObjects>().unselected();
                    }
                   
                    
                    
                }
                if (moved)
                {
                    moved = false;
                }
                selection = null;
            }

            
        }

        if (Input.touchCount == 2)
        {
            Debug.Log("2 Toques");
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
    }


    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

}
