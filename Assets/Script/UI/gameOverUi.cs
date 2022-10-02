using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameOverUi : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup canvasGroup;

    public GameObject Green;
    public GameObject Red;
    public GameObject Blue;

    public GameObject totalScore;
    public GameObject walked;

    public void Awake()
    {
        canvasGroup.alpha = 0;

    }

    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void Ending()
    {
        SetAlpha(1);
        Vector3 currentPos = this.gameObject.transform.position;
        transform.position = new Vector3(currentPos.x, currentPos.y + 5, currentPos.z);
        LeanTween.moveLocalY(this.gameObject, currentPos.y, 2).setEaseInOutBack();

        walked.GetComponent<TextMeshProUGUI>().SetText("You Runned: " + Global.Runned + " M");

        totalScore.GetComponent<TextMeshProUGUI>().SetText("Score: " + Global.Score);

        if (Global.enemyKilled.ContainsKey("green"))
        {
            Green.GetComponent<TextMeshProUGUI>().SetText("X " + Global.enemyKilled["green"]);
        }
        else Green.GetComponent<TextMeshProUGUI>().SetText("X 0");

        if (Global.enemyKilled.ContainsKey("red"))
        {
            Red.GetComponent<TextMeshProUGUI>().SetText("X " + Global.enemyKilled["red"]);
        }
        else Red.GetComponent<TextMeshProUGUI>().SetText("X 0");

        if (Global.enemyKilled.ContainsKey("blue"))
        {
            Blue.GetComponent<TextMeshProUGUI>().SetText("X " + Global.enemyKilled["blue"]);
        }
        else Blue.GetComponent<TextMeshProUGUI>().SetText("X 0");
    }
}
