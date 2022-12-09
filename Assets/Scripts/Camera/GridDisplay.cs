using UnityEngine;
using UnityEngine.EventSystems;

public class GridDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public bool drawGrid = false;
    public InitializeGrid test;
    private Grid grid;
    private int[,] cuadricula;
    private int filas;
    private int columnas;

    private Vector3 Origin;
    private Vector3 Diference;
    void Start()
    {
        grid = test.getGrid();
        cuadricula = test.getCuadricula();
        filas = test.getFilas();

        columnas = test.getColumnas();
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPostRender()
    {
        if (drawGrid)
        {
            for (int i = 0; i < cuadricula.GetLength(0); i++)
            {

                for (int j = 0; j < cuadricula.GetLength(1); j++)
                {
                    DrawLine(grid.GetWorldPosition(i, j) - new Vector3(filas * 2.5f, 0, columnas * 2.5f), grid.GetWorldPosition(i, j + 1) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));
                    DrawLine(grid.GetWorldPosition(i, j) - new Vector3(filas * 2.5f, 0, columnas * 2.5f), grid.GetWorldPosition(i + 1, j) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));

                }
            }
            DrawLine(grid.GetWorldPosition(0, columnas) - new Vector3(filas * 2.5f, 0, columnas * 2.5f), grid.GetWorldPosition(filas, columnas) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));
            DrawLine(grid.GetWorldPosition(filas, 0) - new Vector3(filas * 2.5f, 0, columnas * 2.5f), grid.GetWorldPosition(filas, columnas) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));
        }
    }


    void DrawLine(Vector3 inicio, Vector3 fin)
    {

        GL.Begin(GL.LINES);
        GL.Color(Color.black);
        GL.Vertex(inicio);
        GL.Vertex(fin);

        GL.End();

    }


}
