using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour {

    [HideInInspector] public MasterReferences master;

    public float respawnChance;
        //1 = 100%, 2 = 50%, ....

    private void OnCollisionEnter2D(Collision2D coll)
    {
        CheckCollision(coll);
    }

    void CheckCollision(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ally" || coll.gameObject.tag == "Player")
        {
            KillEnemy();
        }
    }

    void KillEnemy ()
    {
        master.scorer.AddEnemyDeath();
        bool chance = Random.Range(0, respawnChance) <= 1;
        Debug.Log(Random.Range(0, respawnChance) + " chance" + chance);
        if (chance)
        {
            master.spawnEnemies.AddSpawn(1);
        }
        Destroy(gameObject);
    }

}
