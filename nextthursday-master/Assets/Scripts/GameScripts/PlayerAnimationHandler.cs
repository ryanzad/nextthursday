using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour {

    SpriteAnim sprAnim;
    public TargetHandler targetHandler;


    TargetHandler.FormationMode lastFormation = TargetHandler.FormationMode.FOLLOW_CURSOR;
    
	void Start () {
        sprAnim = GetComponent<SpriteAnim>();
        sprAnim.Play(0);
    }
	
	// Update is called once per frame
	void Update () {

        TargetHandler.FormationMode getFormation = targetHandler.GetFormationMode();


        if (getFormation != lastFormation)
        {
            lastFormation = getFormation;
            ChangeFormation(getFormation);
        }
	}


    void ChangeFormation (TargetHandler.FormationMode form)
    {
        switch (form)
        {
            case TargetHandler.FormationMode.LINE:
                sprAnim.Play("player/queue", 0);
                break;

            case TargetHandler.FormationMode.FOLLOW_CURSOR:
                sprAnim.Play("player/walk", 0);
                break;

        }

    }
}
