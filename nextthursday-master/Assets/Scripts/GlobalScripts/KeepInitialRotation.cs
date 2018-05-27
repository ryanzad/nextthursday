using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInitialRotation : MonoBehaviour {

    Quaternion initRot;
    bool allow = false;

	void Start () {
        initRot = transform.rotation;
        allow = true;
    }
	
	void Update () {
		if (allow)
        {
            transform.rotation = initRot;
        }
        
	}
}
