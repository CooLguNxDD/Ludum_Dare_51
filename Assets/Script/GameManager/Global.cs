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
    public static float EnemyHpMutiplyer;
    public static float EnemyDamageMutiplyer;

    //enemy hp bar
    public static bool isEnemyHpBarActive = false;

    //preset arrows
    public static List<Arrows> presetArrows;
    public GameObject UpPrefab;
    public GameObject DownPrefab;
    public GameObject LeftPrefab;
    public GameObject RightPrefab;

    //game stats:
    public static Dictionary<string, int> enemyKilled;
    public static int Score;
    public static int Runned;

    public static int HPRegenerateRate; 

    //GameOver
    public static bool GameOver =  false;

    //wave
    public static float wavePerRound;

    void Start()
    {
        presetArrows = new List<Arrows>();
        presetArrows.Add(new Arrows("right", RightPrefab));
        presetArrows.Add(new Arrows("left", LeftPrefab));
        presetArrows.Add(new Arrows("up", UpPrefab));
        presetArrows.Add(new Arrows("down", DownPrefab));

        Reset();

        DontDestroyOnLoad(this);
    }

    public void Reset()
    {
        Debug.Log("Resetting");
        EnemyHpMutiplyer = 1f;
        EnemyDamageMutiplyer = 1f;

        ArrowsSpawningQueue = new Queue<Arrows>();
        HoldingObject = new Queue<Arrows>();
        enemyKilled = new Dictionary<string, int>();

        Score = 0;
        Runned = 0;

        spawnNumber = 8;
        HPRegenerateRate = 2;
        wavePerRound = 18;
        player = playerCurrent;
        TileMap = TileMapCurrent;
        isEmeny = false;

        if (playerCurrent) player = playerCurrent;
        else player = GameObject.FindGameObjectWithTag("player");
        if (TileMapCurrent) TileMap = TileMapCurrent;
        else TileMap = GameObject.FindGameObjectWithTag("Tile");

        if (player)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
    }
}
