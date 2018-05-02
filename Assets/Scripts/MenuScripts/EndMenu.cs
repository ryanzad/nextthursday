using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour {

    public TextMesh score;
    
	void Start () {
        score.text = "Score: " + PlayerPrefs.GetInt("GameScore");
        StartCoroutine(End());
	}

    IEnumerator End ()
    {
        yield return new WaitForSeconds(5);
        Application.LoadLevel("MainMenu");
        PlayerPrefs.DeleteAll();

    }
	
}
