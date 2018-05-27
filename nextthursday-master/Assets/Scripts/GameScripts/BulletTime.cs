using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour {

    public float minBulletDistance = 0.1f, maxBulletDist = 1f, minTimeSlow = 0.1f, maxTimeSlow = 1f;

    
	void Update () {

        float bulletDist = GetBulletDist();

        if (bulletDist < maxBulletDist)
        {
            float mappedTime = map(bulletDist, minBulletDistance, maxBulletDist, minTimeSlow, maxTimeSlow);
            Time.timeScale = mappedTime < minTimeSlow ? minTimeSlow : mappedTime;
        } else
        {
            Time.timeScale = 1;
        }


	}

    float GetBulletDist ()
    {
        List<GameObject> bullets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bullet"));

        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject bullet in bullets)
        {
            float distance = Vector3.Distance(transform.position, bullet.transform.position);
            bool isNotMe = bullet != this.gameObject;

            if (distance < minDist && isNotMe)
            {
                minDist = distance;
            }
        }

        Debug.Log("MIN BULLET DIST: " + minDist);

        return minDist;
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
