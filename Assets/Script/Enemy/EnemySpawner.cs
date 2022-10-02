using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] SpawnList;
    public GameObject enemyGroup;

    private void Start()
    {
        enemyGroup = GameObject.Find("enemyGroup");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entered!!!!!!!!!!!!!!!");
        if (collision.CompareTag("Player"))
        {
            int random = Random.Range(0, SpawnList.Length);

            GameObject obj = Instantiate(SpawnList[random], transform.position, Quaternion.identity);
            obj.transform.SetParent(enemyGroup.transform);
            Destroy(this.gameObject);
        }
    }

}
