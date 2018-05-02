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
    public AnimationCurve difficultyCurve;

}
