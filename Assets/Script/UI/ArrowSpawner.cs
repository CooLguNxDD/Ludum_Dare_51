using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] spawnPoint;

    
    private int CurrentSpawnedCount = 0;
    private bool isSpawning = false;


    public float spawnSpeed = 0.25f;

    public Queue<Arrows> HoldingObject;

    
    private void Awake()
    {
        isSpawning = false;
    }
    void Start()
    {
        //todo random;
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.ArrowsSpawningQueue.Count > 0 && !isSpawning && CurrentSpawnedCount < 6)
        {
            //Debug.Log("doing queue"+ Global.ArrowsSpawningQueue.Count);

            Arrows arrow = Global.ArrowsSpawningQueue.Dequeue();
            StartCoroutine(spawnArrowUI(arrow));
        }
    }
    public void RemoveOneArrow()
    {
        Arrows current = Global.HoldingObject.Dequeue();
        StartCoroutine(destroyArrow(current.getObject()));

        int Counter = 0;
        CurrentSpawnedCount -= 1;
        foreach (Arrows arrow in Global.HoldingObject)
        {
            LeanTween.moveX(arrow.getObject(), spawnPoint[Counter].transform.position.x, spawnSpeed);
            Counter += 1;
        }
    }
    IEnumerator destroyArrow(GameObject Obj)
    {
        LeanTween.moveY(Obj, 20, spawnSpeed);
        yield return new WaitForSeconds(spawnSpeed);

        Destroy(Obj);
        yield return null;
    }
    IEnumerator spawnArrowUI(Arrows arrows)
    {
        isSpawning = true;

        //spawn arrow from queue;
        GameObject newArrow = Instantiate(arrows.getObject());
        newArrow.transform.SetParent(transform, false);
        float y = transform.position.y;
        Debug.Log(y);
        newArrow.transform.position = new Vector3(spawnPoint[CurrentSpawnedCount].transform.position.x, 15, transform.position.z);
        Debug.Log(y);

        //animation
        LeanTween.moveY(newArrow, 8, 1).setEaseOutBounce();


        Global.HoldingObject.Enqueue(new Arrows(arrows.getDirection(), newArrow));
        CurrentSpawnedCount += 1;
        yield return new WaitForSeconds(spawnSpeed);
        isSpawning = false;
 
    }
}
