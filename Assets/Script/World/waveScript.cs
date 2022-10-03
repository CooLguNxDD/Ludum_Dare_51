using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float waveMoveSpeed = 0.5f;

    private Vector3 nextPos;
    private bool isNextWave;
    private float waveOffset = 0f;
    void Start()
    {
        nextPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNextWave)
        {
            StartCoroutine(waveMovement());
        }
        if (transform.position.y < nextPos.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * waveMoveSpeed * (waveOffset), transform.position.z);
        }
    }

    IEnumerator waveMovement()
    {
        isNextWave = true;
        waveOffset = (Mathf.Log(Global.wavePerRound  + 2));
        yield return new WaitForSeconds(10);
        nextPos = new Vector3(transform.position.x, transform.position.y + Global.wavePerRound, transform.position.z);
        isNextWave = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(100);
        }
    }
}
