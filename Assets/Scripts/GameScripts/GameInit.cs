using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

    public MasterReferences master;

    public List<GameObject> levels = new List<GameObject>();

    int level = 0;



    [Header("REFERENCES")]
    
    public GameObject IntroSelectionPrefab;
    public GameObject PlayerPrefab;


    [Header("INTERNAL REFERENCES")]

    GameObject player;
    MoveMotor playerMotor;





    void Start ()
    {
        InitLevel();
    }

    public void InitLevel () {
        level = PlayerPrefs.HasKey("LevelLoad") ? PlayerPrefs.GetInt("LevelLoad") : 0;
        LoadSelection();
	}

    void LoadSelection ()
    {
        GameObject introSelectionObj = Instantiate(IntroSelectionPrefab);
        SelectionHandler selectionHandler = introSelectionObj.GetComponent<SelectionHandler>();
        selectionHandler.modifiers = master.modifiers;
        selectionHandler.gameInit = this;
        selectionHandler.Init(level);
    }

    public void StartGame () //call this to start the game
    {
        LevelData levelData = LoadLevel();
        SpawnGame(levelData);

        SetCameraTarget(player.transform);
        playerMotor.On();
    }

    LevelData LoadLevel()
    {
        GameObject levelObj = Instantiate(levels[level]);
        return levelObj.GetComponent<LevelData>();
    }


    void SpawnGame(LevelData levelData)
    {

        SpawnPlayer(levelData);
        SpawnBuildings(levelData);
        SpawnSceneObjects(levelData);
        
    }

    void SpawnPlayer (LevelData levelData)
    {
        player = Instantiate(PlayerPrefab, levelData.playerSpawn);
        playerMotor = player.GetComponent<MoveMotor>();
    }

    void SpawnBuildings(LevelData levelData)
    {
        foreach (LevelData.Building building in levelData.buildings)
        {
            foreach (Transform child in levelData.transform)
            {
                if (child.name.Contains(building.keyTerm))
                {
                    Instantiate(building.buildingPrefab, child);
                }
            }

        }
    }
    
    void SpawnSceneObjects(LevelData levelData)
    {
        foreach (LevelData.SceneObjects sceneObject in levelData.sceneObjects)
        {
            foreach (Transform child in levelData.transform)
            {
                if (child.name.Contains(sceneObject.keyTerm))
                {
                    Instantiate(sceneObject.sceneObjectPrefab, child);
                }
            }
        }
    }


    void SetCameraTarget (Transform target)
    {
        master.camFollow.SetTarget(target);
    }
    

}
