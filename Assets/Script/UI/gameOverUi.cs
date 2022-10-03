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

    public Button button;

    public void Awake()
    {
        canvasGroup.alpha = 0;
        button.enabled = false;

    }

    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void Ending()
    {
        button.enabled = true;
        SetAlpha(1);

        RectTransform rect = this.GetComponent<RectTransform>();

        Vector3 currentPos = this.GetComponent<RectTransform>().position;

        rect.position = new Vector3(rect.position.x, rect.position.y + 10, rect.position.z);

        LeanTween.moveY(rect, -16, 2).setEaseInOutBack();

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
