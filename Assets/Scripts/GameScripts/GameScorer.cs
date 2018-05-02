using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScorer : MonoBehaviour {

    public MasterReferences master;
    int allyCount = 0, enemyDeathCount;


    public void AddAlly ()
    {
        allyCount++;
    }

    public void KillAlly()
    {
        allyCount--;
    }

    public void AddEnemyDeath ()
    {
        enemyDeathCount++;
    }

    public int GetAllyCount ()
    {
        return allyCount;
    }

    public int GetEnemyDeaths()
    {
        return enemyDeathCount;
    }
}
