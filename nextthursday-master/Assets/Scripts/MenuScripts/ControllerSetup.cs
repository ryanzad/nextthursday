using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSetup : MonoBehaviour {

    public enum Controls { MOUSE, X360 };
    public Controls control;


    void Start () {
        PlayerPrefs.SetString("Controls", control.ToString());	
	}
}
