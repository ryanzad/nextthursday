using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlow : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Ally")
        {
            coll.gameObject.GetComponent<MoveMotor>().isInWater(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Ally")
        {
            coll.gameObject.GetComponent<MoveMotor>().isInWater(false);
        }
    }


}
