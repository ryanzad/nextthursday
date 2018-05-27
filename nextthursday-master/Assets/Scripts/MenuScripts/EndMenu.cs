using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour {

    public TextMesh score;
    public TextMesh gameComplete;

    void Start () {
        string gameState = PlayerPrefs.GetString("GameEndState");
        if (gameState == "WIN")
        {
            gameComplete.text = "You have won!";
        }
        else if (gameState == "DEATH")
        {
            gameComplete.text = "You have died.";
        }

        score.text = "Score: " + PlayerPrefs.GetInt("GameScore");
        StartCoroutine(End());
	}

    IEnumerator End ()
    {
        yield return new WaitForSeconds(5);
        GetComponent<ResetGame>().Reset();
        Application.LoadLevel("MainMenu");

    }
	
}
