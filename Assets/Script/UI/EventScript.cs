using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Score;
    public GameObject Event;

    private bool isSet = false;

    // Update is called once per frame
    void Update()
    {

        
        Score.GetComponent<TextMeshProUGUI>().SetText("Score: " + Global.Score);

        if (!isSet)
        {
            StartCoroutine(setEventName());
        }
        
    }

    IEnumerator setEventName()
    {
        isSet = true;
        Event.GetComponent<TextMeshProUGUI>().SetText("Event: " + Global.currentEvent.name);
        yield return new WaitForSeconds(0.5f);
        isSet = false;

    }
}
