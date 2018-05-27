using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearChildren : MonoBehaviour {

    public float delay;
    public bool allowMouseSkip;
    public float delayMouseSkip;

    void Start () {
        SetChildren(false);
        StartCoroutine(Delay(delay));
    }

    private void Update()
    {
        if (allowMouseSkip)
        {
            if (Input.GetMouseButton(0))
            {
                StopAllCoroutines();
                allowMouseSkip = false;
                StartCoroutine(Delay(delayMouseSkip));
            }
        }
    }

    IEnumerator Delay (float delay)
    {
        yield return new WaitForSeconds(delay);
        SetChildren(true);
    }


    void SetChildren (bool state)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}
