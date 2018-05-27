using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour {

    public EnemyMotor motor;
    public SpriteAnim sprAnim;

    bool foundState = false;

	void Update () {

        bool getFound = motor.GetFoundTarget();



        if (getFound != foundState)
        {
            foundState = getFound;
            if (foundState)
            {
                sprAnim.Play("enemy/wand", 0);
            }
            else
            {
                sprAnim.Play("enemy/scan", 0);
            }

        }

	}
}
