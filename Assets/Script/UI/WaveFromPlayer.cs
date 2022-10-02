using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveFromPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject wave;
    public GameObject distanceText;
    public float tweenTime;

    private float distance;
    private TextMeshProUGUI text;
    private bool Tweening = false;

    void Awake()
    {
        distance = 0;
        Tweening = false;
        text = distanceText.GetComponent<TextMeshProUGUI>();
        
    }
    public void TweenUp()
    {
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), tweenTime).setEaseInSine();
    }
    public void TweenDown()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), tweenTime).setEaseInSine();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = player.transform.position.y - wave.transform.position.y - 10;
        
        if (distance < 15 && !Tweening)
        {
            StartCoroutine(TweenAdjustor());
            text.color = Color.red;
        }
        else if (distance > 15) text.color = Color.blue;

        text.SetText("WAVE: " + (int)distance + " M");

    }
    IEnumerator TweenAdjustor()
    {
        Tweening = true;
        TweenUp();
        yield return new WaitForSeconds(tweenTime);
        TweenDown();
        yield return new WaitForSeconds(tweenTime);
        Tweening = false;


    }
}
