using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    public CanvasGroup canvasGroup;
    private int currentHealth = 0;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }
    public void CurrentHealthMinusOne()
    {
        currentHealth -= 1;
        slider.value = currentHealth;

        if (currentHealth <= 0) {
            canvasGroup.alpha = 0;
        }
    }
    public void SetCurrentHealth(int health)
    {
        slider.value = health;
        currentHealth = health;
    }
    public void Awake()
    {
        canvasGroup.alpha = 0;
    }
    public void SetHPBarAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
