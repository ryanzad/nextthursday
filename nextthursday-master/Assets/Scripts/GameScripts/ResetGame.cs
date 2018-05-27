using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour {

    public void Reset()
    {
        PlayerPrefs.DeleteKey("LevelLoad");
        PlayerPrefs.DeleteKey("ModList");
        PlayerPrefs.DeleteKey("Allies");
        PlayerPrefs.DeleteKey("GameScore");
        PlayerPrefs.DeleteKey("GameEndState");
    }
}
