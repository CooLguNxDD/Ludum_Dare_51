using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCurrent;
    [SerializeField]
    private GameObject TileMapCurrent;

    //Global variable
    public static GameObject player;
    public static GameObject TileMap;

    //enable battle queue
    public static bool isEmeny;

    public static Queue<Arrows> ArrowsSpawningQueue;
    public static Queue<Arrows> HoldingObject;

    [SerializeField]
    public static int spawnNumber;

    //enemy:
    public static float EnemyHpMutiplyer = 1f;
    public static float EnemyDamageMutiplyer = 1f;

    //enemy hp bar
    public static bool isEnemyHpBarActive = false;

    //preset arrows
    public static List<Arrows> presetArrows;
    public GameObject UpPrefab;
    public GameObject DownPrefab;
    public GameObject LeftPrefab;
    public GameObject RightPrefab;

    void Start()
    {
        ArrowsSpawningQueue = new Queue<Arrows>();
        HoldingObject = new Queue<Arrows>();

        presetArrows = new List<Arrows> (); 
        presetArrows.Add(new Arrows("right", RightPrefab));
        presetArrows.Add(new Arrows("left", LeftPrefab));
        presetArrows.Add(new Arrows("up", UpPrefab));
        presetArrows.Add(new Arrows("down", DownPrefab));

        spawnNumber = 10;
        player = playerCurrent;
        TileMap = TileMapCurrent;
        DontDestroyOnLoad(this);
    }
    public void Reset()
    {
        EnemyHpMutiplyer = 0.5f;
        EnemyDamageMutiplyer = 0.5f;
        ArrowsSpawningQueue = new Queue<Arrows>();
        HoldingObject = new Queue<Arrows>();
        spawnNumber = 10;
        player = playerCurrent;
        TileMap = TileMapCurrent;
        isEmeny = false;
    }
}
