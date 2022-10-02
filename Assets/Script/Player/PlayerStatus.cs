using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int HP = 100;

    public int GetHP() { return HP; }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log(HP);
    }
    

}
