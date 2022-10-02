using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int HP = 100;
    private float animationHP;

    public GameObject playerHPBar;
    public GameObject playerHPBarInner;

    public int GetHP() { return HP; }

    private float value;
    private float currentDamage;

    public void TakeDamage(int damage)
    {
        HP -= damage;
        currentDamage = damage;
        StartCoroutine(takeDamageAnimation());
    }

    public void Update()
    {
        if (HP < animationHP)
        {
            value = currentDamage * Time.deltaTime * 10;
            animationHP -= value;
            playerHPBar.GetComponent<PlayerHpBar>().SetCurrentHealth(animationHP);
        }
    }
    IEnumerator takeDamageAnimation() {
        playerHPBarInner.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.25f);
        playerHPBarInner.GetComponent<Image>().color = Color.green;
    }
    public void Start()
    {
        animationHP = HP;
        playerHPBar.GetComponent<PlayerHpBar>().SetMaxHealth(HP);
        playerHPBar.GetComponent<PlayerHpBar>().SetHPBarAlpha(0.8f);
        
    }




}
