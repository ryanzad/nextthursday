using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    // STRUCTS

    


    [System.Serializable]
    public struct NPCSpawn
    {
        public GameObject NPCPrefab;
        public NPCHandler.NPCMode mode;
        public string keyTerm;
    }

    [System.Serializable]
    public struct Building
    {
        public GameObject buildingPrefab;
        public string keyTerm;
    }

    [System.Serializable]
    public struct SceneObjects
    {
        public GameObject sceneObjectPrefab;
        public string keyTerm;
    }


    [Header("OBJECT REFERENCES")]
    public Transform playerSpawn;
    public string enemySpawnKey;
    public List<NPCSpawn> npcSpawn = new List<NPCSpawn>();
    public List<Building> buildings = new List<Building>();
    public List<SceneObjects> sceneObjects = new List<SceneObjects>();


    [Header("LEVEL CONTROLS")]
    public bool isTutorial;
    public AnimationCurve difficultyCurve; //realistically bounded to values (e.g. time = number of allies, value = number of enemies spawned in)
    //e.g. point 1 [0,0], point 2 [4, 10] means that by the time you have 4 allies, 10 enemies will spawn. It is capped at 10 enemies.
    public AnimationCurve enemyTimeIncrease; //0,0 to 1,1 curve of speed of the enemies spawned in via game time
    public float enemyTimeMax; //by the end of the round, if no enemies are dead there should be this amount of enemies spawned in
    public float spawnInterval = 0.1f;
    public AnimationCurve bulletSpeedIncrease; //additional bullet speed over time (time = time, value = extra bullet speed)
    public AnimationCurve bulletIntervalMulti; //bullet interval multiplier over time (time = time, value = extra bullet speed)
}
