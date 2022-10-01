using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 1f;


    public int moveGrid = 1;
    
    public bool isMovingX = false;
    public bool isMovingY = false;

    private bool isWall = false;

    public GameObject ArrowSpawnPoint;

    private GameObject Enemy;
    private float endTime = 0;
    private GridLayout grid;
    private Vector3Int PlayerCellPosition;
    private Vector3Int PreviousPlayerCellPosition;


    void Start()
    {
        grid = transform.GetComponentInParent<GridLayout>();
        endTime = 5 / moveSpeed;
    }

    // Update is called once per frame
    void Update()
    { 
        endTime = 5 / moveSpeed;
        Movement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Global.isEmeny = true;
            Enemy = collision.collider.gameObject;
            AddArrowsToQueue();
        }
        else
        {
            isWall = true;
            Debug.Log("enter" + isWall);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isWall = false;
        Debug.Log("exit"+ isWall);
    }

    private void AddArrowsToQueue()
    {
        for (int i = 0; i < Global.spawnNumber; i++)
        {
            int random = Random.Range(0, Global.presetArrows.Count);
            Global.ArrowsSpawningQueue.Enqueue(Global.presetArrows[random]);
        }
    }
    private void Encounter()
    {
        if (Global.HoldingObject.Count == 0 && Global.ArrowsSpawningQueue.Count == 0) {
            Enemy.GetComponent<Enemy>().DestroyIt();
            Global.isEmeny = false;
            return;
        }

        if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "right" &&
            (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))){
            KillOneArrow();
        }
        if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "left" &&
    (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            KillOneArrow();
        }
        if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "up" &&
    (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            KillOneArrow();
        }
        if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "down" &&
    (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            KillOneArrow();
        }

    }
    private void KillOneArrow()
    {
        ArrowSpawnPoint.GetComponent<ArrowSpawner>().RemoveOneArrow();
    }
    private void Movement()
    {
        //collide enemy
        if (Global.isEmeny)
        {
            Encounter();
            PlayerCellPosition = PreviousPlayerCellPosition;
        }
        else
        {
            //collide wall
            if (isWall)
            {
                PlayerCellPosition = PreviousPlayerCellPosition;
            }

            //left right
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && !isMovingX && !isMovingY)
            {
                StartCoroutine(MovementX());
            }
            //up down
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && !isMovingY && !isMovingX)
            {
                StartCoroutine(MovementY());
            }
        }
    }

    private IEnumerator MovementX()
    {
        // move in grid
        PreviousPlayerCellPosition = PlayerCellPosition;
        isMovingX = true;
        PlayerCellPosition += new Vector3Int((int)(Input.GetAxisRaw("Horizontal")) * moveGrid, 0, 0);

        float elapsedTime = 0;
        
        while (elapsedTime < endTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, grid.CellToWorld(PlayerCellPosition), moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMovingX = false;
    }

    private IEnumerator MovementY()
    {
        // move in grid
        PreviousPlayerCellPosition = PlayerCellPosition;
        isMovingY = true;
        PlayerCellPosition += new Vector3Int(0, (int)(Input.GetAxisRaw("Vertical")) * moveGrid, 0);

        float elapsedTime = 0;

        while (elapsedTime < endTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, grid.CellToWorld(PlayerCellPosition), moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isMovingY = false;
    }

}
