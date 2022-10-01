using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //some animation!
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyIt()
    {
        StartCoroutine(OnDestory());
    }

    IEnumerator OnDestory()
    {
        //some animation

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
