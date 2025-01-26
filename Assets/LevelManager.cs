using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public float cellsize = 3;

    public GameObject[] tiles;
    public GameObject plane = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (plane != null)
        {
            Destroy(plane);
        }
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        for (int r = 0; r < rows; r++) // r = rows 
        {
            for(int c = 0; c < columns; c++) // c = columns
            {
                Vector3 position = new Vector3(c * cellsize, 0, r * cellsize);
                
                GameObject tile = tiles[Random.Range(0, tiles.Length)];

                Instantiate(tile, position, Quaternion.identity, transform);
            }
        }

        transform.position = new Vector3 (-27, 0, -27);
    }
}
