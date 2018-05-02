using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotor : MonoBehaviour {

    [Header("REFERENCES")]
    public Rigidbody2D rigid;

    [Header("CONTROLS")]
    public float speed;

	void Update () {
        rigid.AddForce(transform.right * speed);
	}
}
