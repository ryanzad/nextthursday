using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScorer : MonoBehaviour {

    public MasterReferences master;
    int allyCount = 0;


    public void AddAlly ()
    {
        Debug.Log("added ally");
        allyCount++;
    }

    public int GetAllyCount ()
    {
        return allyCount;
    }
}
