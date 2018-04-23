using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour {

    public enum GameState { START, PLAY, PAUSE}

    public GameState gameState = GameState.START;


    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */
}
