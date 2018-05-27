using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroShake : MonoBehaviour {


    public Vector3 speed = new Vector3(0.05f, 0.05f, 0.05f);
    public Vector3 scale = new Vector3(1, 1, 1);
    Vector3 seed;

    Vector3 init;

    bool allow = false;


    private void Start()
    {
        StartCoroutine(Init());

    }

    IEnumerator Init()
    {
        yield return null;
        //yield return new WaitForSeconds(1f);
        seed = new Vector3(Random.Range(0.0f, 1000f), Random.Range(0.0f, 1000f), Random.Range(0.0f, 1000f));

        //Transform[] getObjects = GetComponentsInChildren<Transform>();
        // objects = new List<Transform>(getObjects);
        // objects.Remove(transform);
        
        init = transform.localPosition;
        // objects.

        //foreach (Transform obj in objects)
        // {
        //   init.Add(obj, obj.localPosition);
        // }

        allow = true;
    }


    void Update () {

        if (allow)
        {
            Vector3 force = new Vector3();
            force.x = ((Mathf.PerlinNoise(seed.x + Time.time * speed.x, 0.0F) * 2) - 1) * scale.x;
            force.y = ((Mathf.PerlinNoise(seed.y + Time.time * speed.y, 0.0F) * 2) - 1) * scale.y;
            force.x = ((Mathf.PerlinNoise(seed.z + Time.time * speed.z, 0.0F) * 2) - 1) * scale.z;

            Vector3 forceAdded = force;
            forceAdded.x += (force.y * 0.25f) + (force.z * 0.25f);
            forceAdded.y += (force.x * 0.25f) + (force.z * 0.25f);
            forceAdded.z += (force.x * 0.25f) + (force.y * 0.25f);

            transform.localPosition = init + forceAdded;
        }

    }
}
