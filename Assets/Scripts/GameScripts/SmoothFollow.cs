using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public bool useX, useY, useZ;

    public void SetTarget (Transform getTarget)
    {
        target = getTarget;
    }
    
    void Update()
    {
        if (target) FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 newPos;
        newPos.x = useX ? target.position.x : transform.position.x;
        newPos.y = useY ? target.position.y : transform.position.y;
        newPos.z = useZ ? target.position.z : transform.position.z;
        transform.position = newPos;
    }
}