using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D coll)
    {
        CheckCollision(coll);
    }

    void CheckCollision(Collision2D coll)
    {
        GameObject collObj = coll.gameObject;
        if (collObj.tag == "Ally")
        { //|| collObj.tag == "Player")
            MoveMotor motor = collObj.GetComponent<MoveMotor>();
            motor.DieAlly();
        }

        if (collObj.tag == "NPC")
        {
            MoveMotor motor = collObj.GetComponent<MoveMotor>();
            motor.DieNPC();
        }


        Debug.Log("projectile hit: " + collObj.name);


        Destroy(gameObject);
    }
}
