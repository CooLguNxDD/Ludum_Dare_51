using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 1f;
    public int moveGrid = 1;
    public GameObject ArrowSpawnPoint;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private SpriteRenderer characImage;

    private bool isMovingX = false;
    private bool isMovingY = false;
    private bool isWall = false;

    private GameObject Enemy;
    private float endTime = 0;
    private GridLayout grid;
    private Vector3Int PlayerCellPosition;
    private Vector3Int PreviousPlayerCellPosition;

    public AudioSource walkAudio;

    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource hit3;
    public AudioSource hit4;


    void Start()
    {
        characImage = GetComponent<SpriteRenderer>();
        grid = transform.GetComponentInParent<GridLayout>();
        endTime = 5 / (moveSpeed * Global.PlayerMoveSpeedMutliplyer);

}

    // Update is called once per frame
    void Update()
    { 
        endTime = 5 / (moveSpeed * Global.PlayerMoveSpeedMutliplyer) ;
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
            hit1.Play();
        }
        else if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "left" &&
    (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            KillOneArrow();
            hit2.Play();
        }
        else if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "up" &&
    (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            KillOneArrow();
            hit3.Play();
        }
        else if (Global.HoldingObject.Count > 0 && Global.HoldingObject.Peek().getDirection() == "down" &&
    (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            KillOneArrow();
            hit4.Play();
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.GetComponent<PlayerStatus>().TakeDamage(Global.MissArrowDamage);
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
                walkAudio.Play();
                StartCoroutine(MovementX());
            }
            //up down
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && !isMovingY && !isMovingX)
            {
                walkAudio.Play();
                StartCoroutine(MovementY());
            }
        }
    }
    private IEnumerator MovementX()
    {
        
        // set sprite
        if (Input.GetAxisRaw("Horizontal") > 0) characImage.sprite = right;
        if (Input.GetAxisRaw("Horizontal") < 0) characImage.sprite = left;

        // move in grid
        PreviousPlayerCellPosition = PlayerCellPosition;

        isMovingX = true;
        PlayerCellPosition += new Vector3Int((int)(Input.GetAxisRaw("Horizontal") * moveGrid * (int)Global.PlayerMoveGridMutliplyer), 0, 0);

        float elapsedTime = 0;
        
        while (elapsedTime < endTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, grid.CellToWorld(PlayerCellPosition),
                moveSpeed * Global.PlayerMoveSpeedMutliplyer * Time.deltaTime);
            yield return null;
        }
        isMovingX = false;
    }

    private IEnumerator MovementY()
    {

        // set sprite
        if (Input.GetAxisRaw("Vertical") > 0) characImage.sprite = up;
        if (Input.GetAxisRaw("Vertical") < 0) characImage.sprite = down;

        // move in grid
        PreviousPlayerCellPosition = PlayerCellPosition;
        isMovingY = true;
        PlayerCellPosition += new Vector3Int(0, (int)(Input.GetAxisRaw("Vertical")) * moveGrid * (int)Global.PlayerMoveGridMutliplyer, 0);

        float elapsedTime = 0;

        while (elapsedTime < endTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, grid.CellToWorld(PlayerCellPosition),
                moveSpeed * Global.PlayerMoveSpeedMutliplyer * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (Global.Runned < (int)transform.position.y) {
            Global.Runned = (int)transform.position.y;
            Global.Score += (int)(5 * Global.ScoreMutiply);
        }
        
        
        isMovingY = false;
    }

}
