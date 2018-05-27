using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public MasterReferences master;

    public GameObject EnemyPrefab;

    float spawnTimeCounter;

    public float gameTimeSpawnInterval; //gametime spawn = spawn over time
    float gameTimeSpawnCounter;
    float gameTime;



    [HideInInspector] public LevelData levelData;
    [HideInInspector] public List<Transform> spawnPoints;
    [HideInInspector] public AnimationCurve difficulty;
    [HideInInspector] public AnimationCurve enemyTimeIncrease;
    [HideInInspector] public float gameTimeEnemyMax;
    [HideInInspector] public float spawnInterval; //spawn = spawn based on enemy count


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


            gameTimeSpawnCounter += Time.deltaTime;
            gameTime += Time.deltaTime;

            if (gameTimeSpawnCounter > gameTimeSpawnInterval)
            {
                GameTimeSpawn(gameTime);
                gameTimeSpawnCounter = 0;
            }






        }
    }

    int lastGameTimeSpawn = 0;
    void GameTimeSpawn (float time)
    {
        
        int spawnTotal = Mathf.RoundToInt(enemyTimeIncrease.Evaluate(time / master.controls.weekLength) * gameTimeEnemyMax);
      //  Debug.Log("spawning in " + ((spawnTotal - lastGameTimeSpawn) * gameTimeEnemyMax) + " via game time");
        spawnCount += spawnTotal - lastGameTimeSpawn;
        lastGameTimeSpawn = spawnTotal;

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
     //   Debug.Log("spawning enemy : " + enemyID);

        GameObject enemyObj = Instantiate(EnemyPrefab, spawnPoint);
        enemyObj.transform.localPosition = Vector3.zero;
        EnemyHurt enemyHurt = enemyObj.GetComponent<EnemyHurt>();
        enemyHurt.master = master;

        EnemyMotor motor = enemyObj.GetComponent<EnemyMotor>();
        motor.master = master;
        motor.bulletSpeedMulti += levelData.bulletSpeedIncrease.Evaluate(Time.time);
        motor.bulletIntervalMulti = levelData.bulletIntervalMulti.Evaluate(Time.time);

        enemyID++;
    }
	
}


//curve.Evaluate(time * maxTime)