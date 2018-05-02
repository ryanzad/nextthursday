using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour {
    
    public enum NPCMode { NONCON, ALLY };
    NPCMode mode;

    public MoveMotor motor;

    public Renderer render;


    bool isSeenByCamera = true;


    public bool debug;

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
        if (coll.gameObject.tag == "NPC")
        {
            NPCHandler npcHandler = coll.gameObject.GetComponent<NPCHandler>();
            if (npcHandler.mode == NPCMode.ALLY && isSeenByCamera) //ensures it is visible and an ally
            {
                ConvertToAlly();
            }

        }
        else if (coll.gameObject.tag == "Player")
        {
            ConvertToAlly();
        }
    }
    
    void ConvertToAlly()
    {
        mode = NPCMode.ALLY;
        motor.On();
    }





    void Update()
    {
        if (debug) Debug.Log(isSeenByCamera);
        isSeenByCamera = render.isVisible;
    }



}
