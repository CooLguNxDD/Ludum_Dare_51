using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] spawnPoint;
    public GameObject enemyHPBar;

    private int CurrentSpawnedCount = 0;
    private bool isSpawning = false;

    public float spawnSpeed = 0.25f;

    public Queue<Arrows> HoldingObject;

    public ParticleSystem particle;

    
    private void Awake()
    {
        isSpawning = false;
        particle.Stop();
    }
    void Start()
    {
        //todo random;
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.ArrowsSpawningQueue.Count > 0 && !isSpawning && CurrentSpawnedCount < 6)
        {
            //Debug.Log("doing queue"+ Global.ArrowsSpawningQueue.Count);
            if (Global.isEnemyHpBarActive) {
                SpawnHpBar(Global.ArrowsSpawningQueue.Count);
                Global.isEnemyHpBarActive = false;
            }

            Arrows arrow = Global.ArrowsSpawningQueue.Dequeue();
            StartCoroutine(spawnArrowUI(arrow));
        }

    }
    private void SpawnHpBar(int health)
    {
        enemyHPBar.GetComponent<EnemyHpBar>().SetHPBarAlpha(0.8f);
        enemyHPBar.GetComponent<EnemyHpBar>().SetMaxHealth(health);
        enemyHPBar.GetComponent<EnemyHpBar>().SetCurrentHealth(health);
    }

    public void RemoveOneArrow()
    {
        particle.Play();
        Arrows current = Global.HoldingObject.Dequeue();

        enemyHPBar.GetComponent<EnemyHpBar>().CurrentHealthMinusOne();
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
        LeanTween.moveY(Obj, transform.position.y+30, spawnSpeed);
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
        

        RectTransform rect = newArrow.GetComponent<RectTransform>();
        float target_y = rect.position.y;
        float target_z = rect.position.z;

        RectTransform rectSpawn = spawnPoint[CurrentSpawnedCount].GetComponent<RectTransform>();
        float target_x = rectSpawn.position.x;

        newArrow.transform.position = new Vector3(target_x, target_y + 15, target_z);

        //animation
        LeanTween.moveY(rect, 18, 1).setEaseOutBounce();


        Global.HoldingObject.Enqueue(new Arrows(arrows.getDirection(), newArrow));
        CurrentSpawnedCount += 1;
        yield return new WaitForSeconds(spawnSpeed);
        isSpawning = false;
 
    }
}
