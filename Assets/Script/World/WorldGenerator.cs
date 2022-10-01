using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public GameObject[] waterObject;
    public GameObject[] groundObject;
    public GameObject TileMap;

    public int TileMapSize = 5;
    public int endTile = 3;

    public GameObject player;

    private GridLayout grid;
    private Vector3Int cellPosition;
    // Start is called before the first frame update


    void Start()
    {
        grid = transform.GetComponent<GridLayout>();
        SpawnWater();
    }

    public void Update()
    {
        
    }


    public void SpawnWater()
    {
        for (int i = -TileMapSize; i < TileMapSize; i++)
        {
            for (int j = -TileMapSize; j < TileMapSize; j++)
            {
                Vector3 pos = new Vector3(i, j, -5);
                cellPosition = grid.WorldToCell(pos);
                pos = grid.CellToWorld(cellPosition);
                pos.z = 10;
                GameObject newObject = Instantiate(waterObject[0], pos, Quaternion.identity);
                newObject.transform.parent = TileMap.transform;
            }
        }
    }

    //IEnumerator Spawner()
    //{

    //}

    // Update is called once per frame

}
