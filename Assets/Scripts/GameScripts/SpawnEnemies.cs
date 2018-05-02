using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public MasterReferences master;

    public GameObject EnemyPrefab;

    public float spawnInterval;
    float spawnTimeCounter;


    [HideInInspector] public List<Transform> spawnPoints;
    [HideInInspector] public AnimationCurve difficulty;

    int spawnCount;
    
    bool allow = false;
    

    public void AddSpawn (int count)
    {
        spawnCount += count;
    }



    public void StartSpawn () {
        allow = true;
	}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Spawn();
        }

        if (allow)
        {


            if (spawnCount > 0)
            {
                spawnTimeCounter += Time.deltaTime;

                if (spawnTimeCounter > spawnInterval)
                {
                    spawnTimeCounter = 0;
                    spawnCount -= 1;
                    CreateEnemy();
                }
            }



        }
    }


    int lastSpawn = 0;

    public void Spawn ()
    {
        int currentAllies = master.scorer.GetAllyCount();
        int spawnTotal = Mathf.RoundToInt(difficulty.Evaluate(currentAllies));
        spawnCount += spawnTotal - lastSpawn;
        lastSpawn = spawnTotal;
    }

    
    int enemyID = 0;
    void CreateEnemy ()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Debug.Log("spawning enemy : " + enemyID);

        GameObject enemyObj = Instantiate(EnemyPrefab, spawnPoint);
        enemyObj.transform.localPosition = Vector3.zero;
        EnemyHurt enemyHurt = enemyObj.GetComponent<EnemyHurt>();
        enemyHurt.master = master;


        enemyID++;
    }
	
}
