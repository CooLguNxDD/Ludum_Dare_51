using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{

    public static List<NewEvents> newEvents;
    public static NewEvents currentEvent;
    
    //global object
    [SerializeField]
    private GameObject playerCurrent;
    [SerializeField]
    private GameObject TileMapCurrent;

    public static GameObject player;
    public static GameObject TileMap;

    //enable battle queue
    public static bool isEmeny;
    public static Queue<Arrows> ArrowsSpawningQueue;
    public static Queue<Arrows> HoldingObject;

    //preset arrows
    public static List<Arrows> presetArrows;
    public GameObject UpPrefab;
    public GameObject DownPrefab;
    public GameObject LeftPrefab;
    public GameObject RightPrefab;



    //global variable
    [SerializeField]

    public static int spawnNumber;
    //player
    public static int MissArrowDamage;
    public static int HPRegenerateRate;

    public static float PlayerTakeDamagerMutliplyer;
    public static float PlayerMoveGridMutliplyer;
    public static float PlayerMoveSpeedMutliplyer;

    //enemy:
    public static float EnemyHpMutiplyer;
    public static float EnemyDamageMutiplyer;

    //enemy hp bar
    public static bool isEnemyHpBarActive = false;

    //game stats:
    public static float ScoreMutiply = 1f;
    public static Dictionary<string, int> enemyKilled;
    public static int Score;
    public static int Runned;

    //GameOver
    public static bool GameOver;

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
        PlayerTakeDamagerMutliplyer = 1f;

        PlayerMoveGridMutliplyer = 1f;
        PlayerMoveSpeedMutliplyer = 1f;

        ScoreMutiply = 1f;

        ArrowsSpawningQueue = new Queue<Arrows>();
        HoldingObject = new Queue<Arrows>();
        enemyKilled = new Dictionary<string, int>();


        
        Score = 0;
        Runned = 0;

        MissArrowDamage = 3;
        HPRegenerateRate = 5;

        spawnNumber = 8;

        wavePerRound = 17;
        player = playerCurrent;
        TileMap = TileMapCurrent;
        isEmeny = false;
        isEnemyHpBarActive = false;

        GameOver = false;

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
