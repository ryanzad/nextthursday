using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {
    
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
        selectionHandler.Run(level);


        /*switch (levelToLoad)
         * 
        {
            case 0:
                selectionHandler.Run(SelectionHandler.SelectionMode.INTRO); //the first level will have no selection
                break;
            default:
                selectionHandler.Run(SelectionHandler.SelectionMode.BINARY); //every other level will have two choices
                break;
        }*/
    }

    public void StartGame () //call this to start the game
    {
        LoadLevel(0);

        // SpawnPlayer();
    }


    void LoadLevel(int level)
    {
        Instantiate(levels[level]);

    }





	
    
}
