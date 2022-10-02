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

    public float triggerDistance;

    private float distance;
    private TextMeshProUGUI text;
    private bool Tweening = false;

    public AudioSource BG;
    public AudioSource WaveComing;

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

        if(distance < triggerDistance)
        {
            if (WaveComing.volume < 1) WaveComing.volume += (Time.deltaTime / 5);
            
            BG.volume = 1 * (distance / triggerDistance) * 0.25f;
        }
        else
        {
            if (WaveComing.volume > 0) WaveComing.volume -= (Time.deltaTime);
            
            BG.volume = 0.5f;
        }

        if (Global.GameOver)
        {
            BG.volume = 0.5f;
            WaveComing.volume = 0;
            
        }
        
        if (distance < triggerDistance && !Tweening)
        {
            StartCoroutine(TweenAdjustor());
            text.color = Color.red;
        }
        else if (distance > triggerDistance) text.color = Color.blue;

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
