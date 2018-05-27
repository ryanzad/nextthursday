using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : MonoBehaviour {

    public float AllyScreenshakeStrength, AllyScreenshakeTime;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Ally")
        {
            MoveMotor motor = coll.gameObject.GetComponent<MoveMotor>();
            motor.master.screenshake.Shake(AllyScreenshakeStrength, AllyScreenshakeTime);
            motor.Incinerate();
        }
    }
}
