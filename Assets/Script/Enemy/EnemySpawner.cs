using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] SpawnList;
    public GameObject enemyGroup;

    private GameObject obj;
    private bool isSpawned;

    private void Start()
    {
        enemyGroup = GameObject.Find("enemyGroup");
        isSpawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entered!!!!!!!!!!!!!!!");
        if (collision.CompareTag("Player"))
        {
            if (!isSpawned) {
                isSpawned = true;
                int random = Random.Range(0, SpawnList.Length);
                obj = Instantiate(SpawnList[random], transform.position, Quaternion.identity);
                obj.transform.SetParent(enemyGroup.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Entered!!!!!!!!!!!!!!!");
        if (collision.CompareTag("Player"))
        {
            if (obj && isSpawned)
            {
                Destroy(obj);
            }


        }
    }

}
