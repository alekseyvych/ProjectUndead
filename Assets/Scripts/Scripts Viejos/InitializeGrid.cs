using UnityEngine;
using UnityEngine.EventSystems;

public class InitializeGrid : MonoBehaviour
{
    [SerializeField]
    public int filas;
    [SerializeField]
    public int columnas;
    [SerializeField]
    public float tamañoCelda;

    private Grid grid;
    public int[,] cuadricula;

    private void Awake()
    {
        this.GetComponent<Transform>().localScale = new Vector3(filas / 2, 1, columnas / 2);
        this.GetComponent<Transform>().Rotate(0.0f, -90.0f, 0.0f, Space.Self);
        this.GetComponent<Renderer>().material.mainTextureScale = new Vector2(filas / 2, columnas / 2);
        grid = new Grid(filas, columnas, tamañoCelda);
        cuadricula = grid.getCuadricula();
    }
    void Start()
    {
        Vector3 posicion = this.transform.position;
        posicion.y -= 0.2f;
        transform.position = posicion;
    }

    public int[,] getCuadricula()
    {
        return cuadricula;
    }

    public Grid getGrid()
    {
        return grid;
    }

    public int getColumnas()
    {
        return columnas;
    }

    public int getFilas()
    {
        return filas;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
