using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGameObjectOnAwake : MonoBehaviour {

    public List<GameObject> objs;

    private void Awake()
    {
        foreach(GameObject obj in objs)
        {
            obj.SetActive(false);
        }
    }
}
