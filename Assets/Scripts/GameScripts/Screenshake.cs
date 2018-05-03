using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour {

    float screenShakeAmount;
    float shakeSpeed = 0;

    void Update()
    {
        

        if (screenShakeAmount > 0)
        {
            Vector3 newPos = Random.insideUnitSphere * screenShakeAmount;
            newPos.z = 0;
            transform.localPosition = newPos;

            screenShakeAmount -= Time.deltaTime * shakeSpeed;
        }
        else
        {
            screenShakeAmount = 0.0f;
        }

    }


    public void Shake(float effect, float speed)
    {
        screenShakeAmount = effect;
        shakeSpeed = speed;
    }
}
