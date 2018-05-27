using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public bool useX, useY, useZ;

    public bool useZoom;
    float zoomCounter = 0;
    float zoomFactor;
    public float zoomMax = 5;
    public AnimationCurve zoomCurve;

    public void SetTarget (Transform getTarget)
    {
        target = getTarget;
    }
    
    void Update()
    {
        if (target) FollowTarget();
        if (Input.GetMouseButton(0))
        {
            zoomCounter += Time.deltaTime;
            if (zoomCounter > zoomMax) zoomCounter = zoomMax;
        }
        else
        {
            zoomCounter -= Time.deltaTime * 4;
            if (zoomCounter < 0) zoomCounter = 0;
        }


        zoomFactor = zoomCurve.Evaluate(zoomCounter);

    }

    void FollowTarget()
    {
        Vector3 newPos;

        //Debug.Log(zoomFactor + "   ZOOM!");
        newPos.x = useX ? target.position.x : transform.position.x;
        newPos.y = useY ? target.position.y : transform.position.y;
        newPos.z = useZ ? target.position.z : transform.position.z;
        if (useZoom)
        {
            newPos.z = -10 - zoomFactor;
         //   Debug.Log(newPos.z);
        }

        transform.localPosition = newPos;
    }
}