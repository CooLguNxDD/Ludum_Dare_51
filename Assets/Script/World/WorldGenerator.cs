using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public GameObject[] mapPrefabList;
    public GameObject nextMap;
    public GameObject currentMap;

    public GameObject spawnLine;
    public GameObject endLine;

    public int mapOffset;
    public int playerOffset;

    private GridLayout grid;
    private bool spawned = false;
    // Start is called before the first frame update



    void Start()
    {
        grid = transform.GetComponent<GridLayout>();
    }

    public void Update()
    {
        //spawn next map if it reach the spawner line
        if (spawnLine != null)
        {
            if (spawnLine.GetComponent<SpawnLineController>().isSpawned() && !spawned)
            {
                StartCoroutine(Spawner());
            }
        }
        //destory current map if it reach the end line
        if (endLine != null)
        {
            if (endLine.GetComponent<EndLineController>().isEnded())
            {
                StartCoroutine(DestoryMap());
                currentMap = nextMap;
            }
        }
    }
    IEnumerator DestoryMap()
    {
        Destroy(currentMap);
        yield return null;
    }

    IEnumerator Spawner()
    {
        int random = Random.Range(0, mapPrefabList.Length);
        Debug.Log(mapPrefabList.Length);
        Debug.Log(random);
        //Todo: randomlize the map selector
        GameObject randomMap = mapPrefabList[random];

        // spawn new map 
        spawned = true;

        float x = 0;
        float y = Global.player.transform.position.y + mapOffset;
        float z = 0;
        Vector3 Spawn_Pos = new Vector3(x, y, z);
        nextMap = Instantiate(randomMap, Spawn_Pos, Quaternion.identity);
        nextMap.transform.parent = Global.TileMap.transform;


        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }

}
