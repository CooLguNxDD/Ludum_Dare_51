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


    void Awake()
    {
        player = playerCurrent;
        TileMap = TileMapCurrent;
        DontDestroyOnLoad(this);
    }
}
