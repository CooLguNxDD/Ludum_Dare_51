using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvent : MonoBehaviour
{

    // Start is called before the first frame update
    private float waitSecond = 10.5f;

    private bool isEventOccurs;
    void Start()
    {
        isEventOccurs = false;
        Global.newEvents = new List<NewEvents>();

        Global.newEvents.Add(new NewEvents(" Enemy has 3x damage !!!", EnemyDamageMulti(3f)));
        Global.newEvents.Add(new NewEvents(" Enemy has 2x damage !!!", EnemyDamageMulti(2f)));
        Global.newEvents.Add(new NewEvents(" Enemy has 0.5x damage !!!", EnemyDamageMulti(0.5f)));

        Global.newEvents.Add(new NewEvents(" player now takes 3x damage !!!", PlayerDamageTake(3f)));
        Global.newEvents.Add(new NewEvents(" player now takes 2x damage !!!", PlayerDamageTake(2f)));
        Global.newEvents.Add(new NewEvents(" player now takes 0.5x damage !!!", PlayerDamageTake(0.5f)));

        Global.newEvents.Add(new NewEvents(" player has 3x speed !!!", PlayerSpeedMulti(3f)));
        Global.newEvents.Add(new NewEvents(" player has 2x speed !!!", PlayerSpeedMulti(2f)));
        Global.newEvents.Add(new NewEvents(" player has 0.5x speed !!!", PlayerSpeedMulti(0.5f)));

        Global.newEvents.Add(new NewEvents(" Enemy has DOUBLE HP !!!", DoubleEnemyHP()));
        Global.newEvents.Add(new NewEvents(" Enemy has HALF HP !!!", halfEnemyHP()));

        Global.newEvents.Add(new NewEvents(" Mis-Clicked Arrow deals 10 Damage !!!", ArrowDamage(10)));
        Global.newEvents.Add(new NewEvents(" Mis-Clicked Arrow deals 5 Damage", ArrowDamage(5)));
        Global.newEvents.Add(new NewEvents(" Mis-Clicked Arrow deals 1 Damage", ArrowDamage(1)));

        Global.newEvents.Add(new NewEvents(" Health Regeneration ++ !!!", Regeneration(20)));
        Global.newEvents.Add(new NewEvents(" Health Regeneration + !!!", Regeneration(8)));
        Global.newEvents.Add(new NewEvents(" Health Regeneration + !!!", Regeneration(8)));
        Global.newEvents.Add(new NewEvents(" Health Regeneration -", Regeneration(1)));

        Global.newEvents.Add(new NewEvents(" Next Wave will move even faster ++ !!!", WaveMove(25)));
        Global.newEvents.Add(new NewEvents(" Next Wave will move faster + !!!", WaveMove(20)));
        Global.newEvents.Add(new NewEvents(" Next Wave will move slower!!!", WaveMove(10)));
        Global.newEvents.Add(new NewEvents(" Next Wave will stop!!!", WaveMove(0)));


        Global.newEvents.Add(new NewEvents(" No Event ", noEvent()));
        Global.newEvents.Add(new NewEvents(" No Event ", noEvent()));
        Global.newEvents.Add(new NewEvents(" No Event ", noEvent()));
        Global.newEvents.Add(new NewEvents(" No Event ", noEvent()));
        Global.newEvents.Add(new NewEvents(" No Event ", noEvent()));

        Global.newEvents.Add(new NewEvents(" Score 5x", ScoreMultiply(5f)));
        Global.newEvents.Add(new NewEvents(" Score 3x", ScoreMultiply(3f)));
        Global.newEvents.Add(new NewEvents(" Score 2x", ScoreMultiply(2f)));

        //bugged
        //Global.newEvents.Add(new NewEvents(" player now move 3 Grids/Move !!!", PlayerGridMulti(3f)));
        //Global.newEvents.Add(new NewEvents(" player now move 2 Grids/Move !!!", PlayerGridMulti(2f)));

    }
    IEnumerable ScoreMultiply(float multi)
    {
        Global.ScoreMutiply = multi;
        yield return new WaitForSeconds(waitSecond);
        Global.ScoreMutiply = 1f;
        isEventOccurs = false;
    }

    IEnumerable WaveMove(int wave)
    {
        Global.wavePerRound = wave;
        yield return new WaitForSeconds(waitSecond);
        Global.wavePerRound = 17;
        isEventOccurs = false;
    }
    IEnumerable Regeneration(int hp)
    {
        Global.HPRegenerateRate = hp;
        yield return new WaitForSeconds(waitSecond);
        Global.HPRegenerateRate = 5;
        isEventOccurs = false;
    }
    IEnumerable ArrowDamage(int damage)
    {
        Global.MissArrowDamage = damage;
        yield return new WaitForSeconds(waitSecond);
        Global.MissArrowDamage = 3;
        isEventOccurs = false;
    }
    IEnumerable PlayerSpeedMulti(float mulit)
    {
        Global.PlayerMoveSpeedMutliplyer = mulit;
        yield return new WaitForSeconds(waitSecond);
        Global.PlayerMoveSpeedMutliplyer = 1f;
        isEventOccurs = false;
    }
    IEnumerable PlayerGridMulti(float mulit)
    {
        Global.PlayerMoveGridMutliplyer = mulit;
        yield return new WaitForSeconds(waitSecond);
        Global.PlayerMoveGridMutliplyer = 1f;
        isEventOccurs = false;
    }

    IEnumerable PlayerDamageTake(float multiply)
    {
        Global.PlayerTakeDamagerMutliplyer = multiply;
        yield return new WaitForSeconds(waitSecond);
        Global.PlayerTakeDamagerMutliplyer = 1f;
        isEventOccurs = false;
    }

    IEnumerable DoubleEnemyHP()
    {
        Global.EnemyHpMutiplyer = 2f;
        yield return new WaitForSeconds(waitSecond);
        Global.EnemyHpMutiplyer = 1f;
        isEventOccurs = false;
    }
    IEnumerable halfEnemyHP()
    {
        Global.EnemyHpMutiplyer = 0.5f;
        yield return new WaitForSeconds(waitSecond);
        Global.EnemyHpMutiplyer = 1f;
        isEventOccurs = false;
    }

    IEnumerable EnemyDamageMulti(float multiply)
    {
        Global.EnemyDamageMutiplyer = multiply;
        yield return new WaitForSeconds(waitSecond);
        Global.EnemyDamageMutiplyer = 1f;
        isEventOccurs = false;
    }



    IEnumerable noEvent()
    {
        yield return new WaitForSeconds(waitSecond);
        isEventOccurs = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isEventOccurs) {
            
            int random = Random.Range(0, Global.newEvents.Count);
            Global.currentEvent = Global.newEvents[random];

            Debug.Log("NextEvent: " + Global.currentEvent.name);

            isEventOccurs = true;
            StartCoroutine(Global.currentEvent.eventRoutine.GetEnumerator());

        }
    }
}
