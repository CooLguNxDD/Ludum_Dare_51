using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLineController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Spawned = false;

    void Awake()
    {
        Spawned = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !Spawned)
        {
            Debug.Log("Player entered spawn line");
            Spawned = true;
            
            Global.spawnNumber += 3;
            Global.wavePerRound += 3;
        }
    }
    public bool isSpawned()
    {
        return Spawned;
    }
}
