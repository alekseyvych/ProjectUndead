using UnityEngine;

public class MovePlaceableObjects : MonoBehaviour
{
    private InitializeGrid grid;
    private PlaceableObject placeableObject;
    private float zCoord;

    public bool isSelected = true;
    private bool isColliding = false;

    public static bool globalSelection = false;
    private Vector3 position = new Vector3();


    private void Start()
    {
        placeableObject = this.gameObject.GetComponent<PlaceableObject>();
        grid = GameObject.FindWithTag("Floor").GetComponent<InitializeGrid>();
    }


    void OnMouseDown()
    {

        if (!isSelected && !globalSelection && GlobalVariables.EDIT_MODE)
        {
            isSelected = true;
            globalSelection = true;
            position = transform.position;

        }

        else if (!isSelected && !globalSelection && GlobalVariables.DELETE_MODE)
        {
            Destroy(this.gameObject);
        }

        else
        {
            if (!isColliding && isSelected)
            {
                ChangeMaterialOfChildren(0);
                position = transform.position;

                int x, z;
                GetGridPos(GetMouseWorldPos(), out x, out z);

                Vector3 posicion;
                posicion = GetWorldPosition(x, z);

                isSelected = false;
                globalSelection = false;

                if (placeableObject.getObjectId() == -1)
                {
                    placeableObject.setObjectId(TownController.addPlaceableObject(this.placeableObject));
                }

                Debug.Log(transform.position);
                placeableObject.setPlacedPosition(transform.position);
            }
        }

    }

    private void Update()
    {

        if (isSelected == true)
        {
            zCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;

            int x, z;
            GetGridPos(GetMouseWorldPos(), out x, out z);
            Vector3 posicion;
            posicion = GetWorldPosition(x, z);
            transform.position = new Vector3(posicion.x, 0, posicion.z);

        }
    }

    private void ChangeMaterialOfChildren(int index)
    {
        //Dibujarlo verde
    }



    private Vector3 GetMouseWorldPos()
    {
        //(x,y)
        Vector3 mousePoint = Input.mousePosition;

        //z
        mousePoint.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * grid.tamañoCelda;
    }

    public void GetGridPos(Vector3 posicion, out int x, out int z)
    {
        x = Mathf.FloorToInt(posicion.x / grid.tamañoCelda);
        z = Mathf.FloorToInt(posicion.z / grid.tamañoCelda);
    }


    void OnCollisionStay(Collision col)
    {
        if ((col.gameObject.CompareTag("Building") && isSelected))
        {
            isColliding = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if ((other.gameObject.CompareTag("Building") && isSelected))
        {
            isColliding = false;
        }
    }

}