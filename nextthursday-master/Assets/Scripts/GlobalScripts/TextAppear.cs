using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAppear : MonoBehaviour {

    bool allow = false;
    public bool runOnStart;
    public float speed;
    public float delayToNext;
    float counter;

    string initText;

    public TextMesh thisText;
    public TextAppear useNextText;

    public bool allowMouseSkip;

	void Start () {
        initText = thisText.text;
        thisText.text = "";

        if (runOnStart)
        {
            allow = true;
        }
	}

    public IEnumerator TurnOn (float delay)
    {
        yield return new WaitForSeconds(delay);
        allow = true;
    }

    private void Update()
    {
        if (allow)
        {
            counter += Time.deltaTime;

            EvaluateText(counter / speed);




            if (allowMouseSkip)
            {
                if (Input.GetMouseButton(0))
                {
                    counter += Time.deltaTime;
                    counter += Time.deltaTime;
                    delayToNext = 0;
                }
            }

            if (counter >= speed)
            {
                counter = 0;
                allow = false;
                if (useNextText) StartCoroutine(useNextText.TurnOn(delayToNext));
            }


        }
    }


    void EvaluateText (float percent)
    {
        int textLength = initText.Length;
        int textPoint = Mathf.RoundToInt(percent * textLength);

        thisText.text = initText.Substring(0, textPoint);
    }




}
