using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {


    public MasterReferences master;
    public TextMesh tempDisplayProgress, tempDisplayDay;


    public List<string> days;
    public GameObject calender;
    public List<GameObject> calenderTops;
    public Vector3 calenderTopStart, calenderTopEnd;
    public Vector3 calenderTopStartRot, calenderTopEndRot;
    public GameObject currentCalenderTop;

    bool allow = false;
    
    float weekCount;

    int previousDayCount = -1;


    //when you go to a new day,
    //enable the next calenderTop
    //and enable the current calenderTop animation


    public void StartCount ()
    {
        weekCount = 0;
        calender.active = true;
        allow = true;
    }


    private void Start()
    {
        currentCalenderTop = calenderTops[0];
        calender.active = false;
    }

    void Update () {
        if (allow && !master.controls.isTutorial)
        {



            if (weekCount >= master.controls.weekLength)
            {
                allow = false;
                End();
            }
            else
            {
                weekCount += Time.deltaTime;
                DisplayProgress(weekCount / master.controls.weekLength);

                int dayCount = Mathf.FloorToInt(weekCount / master.controls.weekLength * 7);
                if (dayCount != previousDayCount)
                {
                    previousDayCount = dayCount;
                    DisplayDay(dayCount);
                }

            }



        }		
	}

    void End ()
    {
        master.saveHandler.NextScene();
    }

    void DisplayDay (int day)
    {
        if (day > 0)
        {
            currentCalenderTop = calenderTops[day];
            calenderTops[day - 1].GetComponent<Animator>().enabled = true;
            calenderTops[day].active = true;
        }



        tempDisplayDay.text = "" + GetDay(day);
    }

    void DisplayProgress (float progress)
    {
        //progress = Mathf.FloorToInt(progress * 100f);
        progress *= 7;
        progress = progress % 1;

        if (currentCalenderTop)
        {
            currentCalenderTop.transform.localPosition = Vector3.Lerp(calenderTopStart, calenderTopEnd, progress);
            currentCalenderTop.transform.localEulerAngles = Vector3.Lerp(calenderTopStartRot, calenderTopEndRot, progress);
        }

       // tempDisplayProgress.text = (100 - progress) + "s";
    }


    //HELPER

    string GetDay (int day)
    {
        return days[day];
    }
}
