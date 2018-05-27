using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionDetection : MonoBehaviour {

    [HideInInspector] public Modifiers.Modifier mod;


    [Header("REFERENCES")]

    public SelectionHandler selectionHandler;


    private void OnMouseDown()
    {
        Debug.Log("selected mod: " + mod);
        selectionHandler.Select(mod);
    }
}
