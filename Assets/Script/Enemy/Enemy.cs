using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public int additionalHP = 0;
    public bool canAttack = false;
    public int attackDamage = 5;
    public float attackDuration = 0.5f;
    public int Score;

    public string type;

    private bool isAttacking = false;
    private GameObject player;

    private void Update()
    {
        if (canAttack && player && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        
        yield return new WaitForSeconds(attackDuration);
        player.GetComponent<PlayerStatus>().TakeDamage((int)(attackDamage * Global.EnemyDamageMutiplyer));
        isAttacking = false;
        yield return null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            AddArrowsToQueue();
            Global.isEnemyHpBarActive = true;
            player = collision.gameObject;
        }
    }

    private void AddArrowsToQueue()
    {
        float totalSpawn = (Global.spawnNumber + additionalHP) * Global.EnemyHpMutiplyer;
        if (totalSpawn < 1) { totalSpawn = 1; }

        for (int i = 0; i < (int)totalSpawn; i++)
        {
            int random = Random.Range(0, Global.presetArrows.Count);
            Global.ArrowsSpawningQueue.Enqueue(Global.presetArrows[random]);
        }
    }
    // Update is called once per frame

    public void DestroyIt()
    {
        animator.SetBool("died", true);
        attackDamage = 0;
        this.tag = "Untagged";
        this.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(OnDestory());
    }

    public void Counter()
    {
        if (Global.enemyKilled.ContainsKey(type))
        {
            Global.enemyKilled[type] += 1;
        }
        else
        {
            Global.enemyKilled.Add(type, 1);
        }
    }

    IEnumerator OnDestory()
    {
        //some animation
        Global.Score += (int)(Score * Global.ScoreMutiply);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        Counter();
        
    }
}
