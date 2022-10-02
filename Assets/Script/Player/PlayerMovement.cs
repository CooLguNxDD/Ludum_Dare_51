using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 1f;
    public int moveGrid = 1;
    public GameObject ArrowSpawnPoint;

    private bool isMovingX = false;
    private bool isMovingY = false;
    private bool isWall = false;

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
            Debug.Log("isEmeny" + Global.isEmeny);
        }
        else
        {
            isWall = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isWall = false;

    }


    private void Encounter()
    {
        if (Global.HoldingObject.Count == 0 && Global.ArrowsSpawningQueue.Count == 0) {
            Enemy.GetComponent<Enemy>().DestroyIt();
            Global.isEmeny = false;
            return;
        }

        else if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "right" &&
            (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))){
            KillOneArrow();
        }
        else if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "left" &&
    (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            KillOneArrow();
        }
        else if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "up" &&
    (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            KillOneArrow();
        }
        else if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "down" &&
    (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            KillOneArrow();
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.GetComponent<PlayerStatus>().TakeDamage(1);
        }

    }
    private void KillOneArrow()
    {
        StartCoroutine(EnemyRedAnimation());
        ArrowSpawnPoint.GetComponent<ArrowSpawner>().RemoveOneArrow();
    }

    IEnumerator EnemyRedAnimation()
    {
        Enemy.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.15f);
        Enemy.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(0.05f);
        Enemy.GetComponent<SpriteRenderer>().color = Color.white;
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
