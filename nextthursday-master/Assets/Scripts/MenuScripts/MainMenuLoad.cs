using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoad : MonoBehaviour {

    public string gameScene;

	void Update () {
		if (Input.GetKeyDown(KeyCode.S))
        {
            Application.LoadLevel(gameScene);
        }
	}
}
