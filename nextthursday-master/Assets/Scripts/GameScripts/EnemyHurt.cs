using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour {

    [HideInInspector] public MasterReferences master;
    
    public float respawnChance;
    //1 = 100%, 2 = 50%, ....

    public float explodeRadius;
    //1 = normal size, 2 = double size..

    bool dead = false;
    bool invinsible = true;

    public float enemyInvincibleAtSpawn;
    //how long the enemy is invinsible when they spawn

    public GameObject DeathParticle;
    public GameObject PointParticle;


    private void Start()
    {
        StartCoroutine(LoseInvinsibility());
    }

    IEnumerator LoseInvinsibility ()
    {
        yield return new WaitForSeconds(enemyInvincibleAtSpawn);
        invinsible = false;
    }


    private void OnCollisionEnter(Collision coll)
    {
        CheckCollision(coll);
    }

    void CheckCollision(Collision coll)
    {
        if (master.controls.trampleEnemies && !dead && !invinsible)
        {
            if (coll.gameObject.tag == "Ally" || coll.gameObject.tag == "Player")
            {
                dead = true;
                StartCoroutine( KillEnemy());
            }
        }
    }

    IEnumerator KillEnemy ()
    {
        
        master.scorer.AddEnemyDeath();
        bool chance = Random.Range(0, respawnChance) <= 1;
        Debug.Log(Random.Range(0, respawnChance) + " chance" + chance);
        if (chance)
        {
            master.spawnEnemies.AddSpawn(1);
        }

        tag = "Dead";
        GetComponent<BoxCollider>().size *= explodeRadius;
        yield return new WaitForSeconds(0.1f);

        GameObject particleObject = Instantiate(DeathParticle, transform);
        particleObject.transform.parent = transform.root;
        particleObject.transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);
        

        GameObject pointObject = Instantiate(PointParticle, transform);
        pointObject.transform.parent = transform.parent.parent;
        pointObject.transform.localEulerAngles = Vector3.zero;

        Destroy(gameObject);
    }
    
}
