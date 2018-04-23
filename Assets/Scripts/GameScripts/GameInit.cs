using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

    public MasterReferences master;

    public List<GameObject> levels = new List<GameObject>();

    [Header("REFERENCES")]

    public GameObject IntroSelectionPrefab;


    void Start () {
        int level = PlayerPrefs.HasKey("LevelLoad") ? PlayerPrefs.GetInt("LevelLoad") : 0;

        LoadSelection(level);



	}

    void LoadSelection (int level)
    {
        GameObject introSelectionObj = Instantiate(IntroSelectionPrefab);
        SelectionHandler selectionHandler = introSelectionObj.GetComponent<SelectionHandler>();
        selectionHandler.modifiers = master.modifiers;
        selectionHandler.gameInit = this;
        selectionHandler.Init(level);
    }

    public void StartGame () //call this to start the game
    {
        LoadLevel(0);

        // SpawnPlayer();
    }


    void LoadLevel(int level)
    {
     // use this:   Instantiate(levels[level]);

    }





	
    
}
