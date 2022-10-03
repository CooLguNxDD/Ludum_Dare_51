using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int MaxHP = 100;
    public int HP;

    private float animationHP;

    public GameObject playerHPBar;
    public GameObject playerHPBarInner;
    public GameObject endGameUI;

    public AudioSource Hurt;

    public int GetHP() { return HP; }

    private float value;
    private float currentDamage;

    private bool isRegenerating;
    private bool isEnd;

    private void Awake()
    {
        HP = MaxHP;
        isEnd = false;
        isRegenerating = false;
    }
    public void TakeDamage(int damage)
    {
        HP -= (int)(damage * Global.PlayerTakeDamagerMutliplyer);
        currentDamage = (int)(damage * Global.PlayerTakeDamagerMutliplyer);
        Hurt.Play();
        StartCoroutine(takeDamageAnimation());
        if (HP <= 0 && !isEnd)
        {
            Global.GameOver = true;
            endGameUI.GetComponent<gameOverUi>().Ending();
            isEnd = true;
        }
    }
    IEnumerator HealthRegenerate()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(10);

        if ((HP + Global.HPRegenerateRate) > MaxHP) HP = MaxHP;
        else HP += Global.HPRegenerateRate;

        playerHPBar.GetComponent<PlayerHpBar>().SetCurrentHealth(HP);
        isRegenerating = false;
    }

    public void Update()
    {
        if (HP < animationHP)
        {
            value = Time.deltaTime * 10 * Mathf.Log(currentDamage+1);
            animationHP -= value;
            playerHPBar.GetComponent<PlayerHpBar>().SetCurrentHealth(animationHP);
        }

        if (!isRegenerating)
        {
            StartCoroutine(HealthRegenerate());
        }
    }
    IEnumerator takeDamageAnimation() {
        playerHPBarInner.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.25f * Mathf.Log(currentDamage + 1));
        playerHPBarInner.GetComponent<Image>().color = Color.green;
    }
    public void Start()
    {
        animationHP = HP;
        playerHPBar.GetComponent<PlayerHpBar>().SetMaxHealth(HP);
        playerHPBar.GetComponent<PlayerHpBar>().SetHPBarAlpha(0.8f);
    }
}
