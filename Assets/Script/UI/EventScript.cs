using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Score;
    public GameObject Event;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score.GetComponent<TextMeshProUGUI>().SetText("Score: " + Global.Score);
    }
}
