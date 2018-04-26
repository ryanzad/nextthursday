using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    public Transform playerSpawn;
    public List<Transform> enemySpawn;

    [System.Serializable]
    public struct Building
    {
        public GameObject buildingPrefab;
        public string keyTerm;
    }

    public List<Building> buildings = new List<Building>();


    [System.Serializable]
    public struct SceneObjects
    {
        public GameObject sceneObjectPrefab;
        public string keyTerm;
    }

    public List<SceneObjects> sceneObjects = new List<SceneObjects>();

}
