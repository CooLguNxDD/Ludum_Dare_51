using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLineController : MonoBehaviour
{
    private bool Ended = false;
    void Awake()
    {
        this.Ended = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered spawn line");
            this.Ended = true;
        }
    }
    public bool isEnded()
    {
        return this.Ended;
    }
}
