using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour {

    public MasterReferences master;

    public enum NPCMode { NONCON, ALLY };
    NPCMode mode;

    public MoveMotor motor;

    public Renderer render;


    bool isSeenByCamera = true;


    public bool debug;


    public bool GetVisibility ()
    {
        return isSeenByCamera;
    }


    public void SetMode (NPCMode setMode)
    {
        mode = setMode;
        if (setMode == NPCMode.ALLY)
        {
            ConvertToAlly();
        }

    }

    public NPCMode GetMode()
    {
        return mode;
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        CheckCollision(coll);
        
    }

    void CheckCollision (Collision2D coll)
    {
        if (coll.gameObject.tag == "Ally")
        {
            NPCHandler npcHandler = coll.gameObject.GetComponent<NPCHandler>();
            if (npcHandler.mode == NPCMode.ALLY && isSeenByCamera && mode != NPCMode.ALLY) //ensures it is visible and hit by an ally, and is not an ally
            {
                ConvertToAlly();
            }
        }
        else if (coll.gameObject.tag == "Player" && mode != NPCMode.ALLY) //ensures it is hit by player and is not an ally previously
        {
            ConvertToAlly();
        }
    }
    
    void ConvertToAlly()
    {
        mode = NPCMode.ALLY;
        tag = "Ally";
        master.scorer.AddAlly();
        master.spawnEnemies.Spawn();
        motor.On();


        render.material.color = new Color(0, 1, 0, 0.5f);
    }





    void Update()
    {
        if (debug) Debug.Log(isSeenByCamera);
        isSeenByCamera = render.isVisible;
    }



}
