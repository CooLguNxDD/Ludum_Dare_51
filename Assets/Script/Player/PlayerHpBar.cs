using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public CanvasGroup canvasGroup;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetCurrentHealth(float health)
    {
        slider.value = health;
    }
    public void Awake()
    {
        canvasGroup.alpha = 1;
    }
    public void SetHPBarAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
