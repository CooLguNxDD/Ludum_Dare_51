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

    public bool isWall = false;


    private GridLayout grid;
    private Vector3Int PlayerCellPosition;
    private Vector3Int PreviousPlayerCellPosition;

    void Start()
    {
        grid = transform.GetComponentInParent<GridLayout>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isWall = true;
        Debug.Log("enter" + isWall);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isWall = false;
        Debug.Log("exit"+ isWall);
    }

    void Movement()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && !isMovingX)
        {
            PreviousPlayerCellPosition = PlayerCellPosition;
            isMovingX = true;
            PlayerCellPosition += new Vector3Int((int)(Input.GetAxisRaw("Horizontal")) * moveGrid, 0, 0);
        }
        else if (isMovingX && Input.GetAxisRaw("Horizontal") == 0f)
        {
            PreviousPlayerCellPosition = PlayerCellPosition;
            isMovingX = false;
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && !isMovingY)
        {
            PreviousPlayerCellPosition = PlayerCellPosition;
            isMovingY = true;
            PlayerCellPosition += new Vector3Int(0, (int)(Input.GetAxisRaw("Vertical")) * moveGrid, 0);

        }
        else if (isMovingY && Input.GetAxisRaw("Vertical") == 0f)
        {
            PreviousPlayerCellPosition = PlayerCellPosition;
            isMovingY = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, grid.CellToWorld(PlayerCellPosition), moveSpeed * Time.deltaTime);

        if (isWall)
        {
            PlayerCellPosition = PreviousPlayerCellPosition;
        }
        transform.position = Vector3.MoveTowards(transform.position, grid.CellToWorld(PlayerCellPosition), moveSpeed * Time.deltaTime);

    }
}
