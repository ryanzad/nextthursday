using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour {

    public MoveMotor motor;

    Vector3 target;
    
    public enum FormationMode { FOLLOW_CURSOR, LINE };
    [HideInInspector] public FormationMode formation = FormationMode.FOLLOW_CURSOR;
    FormationMode lastFormation;

    [Header("FORMATIONS")]
    public bool allowFollowCursor;

    public bool allowLine;
    public bool isLineHead;
    public bool lineRefresh;
    public float lineRefreshInterval;
    float lineRefreshCount;
    public float lineUnblockAngle; //the angle of the players line of site to move out the way
    public float lineUnblockDistance; //the distance the players line of site reaches to move out the way
    public float lineUnblockForce;

    public float distToHead;

    Transform lineFollowing, lineFollower;
    
    public bool debug, debug2;

    string controlMode = "";



    void Start ()
    {
        controlMode = PlayerPrefs.GetString("Controls");
    }

    public FormationMode GetFormationMode ()
    {
        return formation;
    }




    public bool isUnmarked ()
    { 
        return lineFollower == null;
    }

    public void Mark (Transform t)
    {
    //  lineFollowed = t;
      //  return new Transform();
    }

    public Vector3 GetTarget ()
    {
        return target;
    }
    
	void Update () {
        
        if(Input.GetMouseButton(0) && controlMode == "MOUSE" || 
            Input.GetButtonDown("A_1") && controlMode == "X360")
        {
            if (allowLine && tag == "Ally" || allowLine && tag == "Player") formation = FormationMode.LINE;
        }
        else
        {
            if (allowFollowCursor) formation = FormationMode.FOLLOW_CURSOR;
        }

        CheckForChangeFormation();
        DoFormation();
    }
    
    void CheckForChangeFormation ()
    {
        if (formation != lastFormation)
        {
            lastFormation = formation;
            ChangeFormation(formation);
        }
    }

    void ChangeFormation (FormationMode newForm)
    {
        if (newForm == FormationMode.FOLLOW_CURSOR)
        {
            ClearLine();
        }
    

        else if (newForm == FormationMode.LINE)
        {

            if (isLineHead)
            {
                SetupFollower();
            }
            
        }

    }

    void ClearAllLineTargets()
    {
        List<GameObject> neighbours = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
        neighbours.Add(GameObject.FindGameObjectWithTag("Player"));

        foreach (GameObject neighbour in neighbours)
        {
            neighbour.GetComponent<TargetHandler>().ClearLine();

        }
    }

    public void ClearLine ()
    {
        lineFollowing = null;
        lineFollower = null;
        distToHead = 0;
        motor.forwardSpeedAdd1 = 0;
    }


    public void SetupFollower()
    {
        GameObject previousAllyObj = GetClosestUnmarked();
        if (previousAllyObj) //can find a closest neighbour
        {

            Transform previousAlly = previousAllyObj.transform;
            TargetHandler previousAllyTarget = previousAlly.GetComponent<TargetHandler>();
            

            previousAllyTarget.distToHead = Vector3.Distance(transform.position, previousAlly.position) + distToHead;


            lineFollower = previousAlly.transform; //sets the follower to be previous player
            previousAllyTarget.lineFollowing = transform; //points previous player to follow you
            previousAllyTarget.SetupFollower();

         /*   if (lineFollowing)
            {
                motor.forwardSpeedAdd1 = Vector3.Distance(transform.position, lineFollowing.position) / 15f;
            }*/

        }


    }


    GameObject GetClosestUnmarked ()
    {
        //initialise variables
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        List<GameObject> neighbours = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
        neighbours.Add(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject neighbour in neighbours)
        {
            float distance = Vector3.Distance(transform.position, neighbour.transform.position);
            bool unmarked = neighbour.GetComponent<TargetHandler>().isUnmarked();
            bool isNotMe = neighbour != this.gameObject;

            if (distance < minDist && unmarked && isNotMe)
            {
                closest = neighbour;
                minDist = distance;
            }
        }
        
        return closest;
    }
    

    void DoFormation()
    {
        if (formation == FormationMode.FOLLOW_CURSOR && allowFollowCursor || isLineHead)
        {
           target = FollowCursorFormation();
        }
        if (formation == FormationMode.LINE && allowLine)
        {
            if (!isLineHead)
            {
                target = LineFormation();
                DontBlockPlayer();
            }
            else
            {
                //Refreshes every interval
                lineRefreshCount += Time.deltaTime;
                if (lineRefreshCount > lineRefreshInterval && lineRefresh)
                {
                    lineRefreshCount = 0;
                    ClearAllLineTargets();
                    SetupFollower();
                }
            }

        }
    }

    void DontBlockPlayer ()
    {
        if (motor.master)
        {
            Transform playerPos = motor.master.player.transform;
            Vector3 directionToTarget = transform.position - playerPos.position;
            float angle = Vector3.Angle(playerPos.right, directionToTarget);
            float distanceToTarget = Vector3.Distance(transform.position, playerPos.position);
            
            if (angle < lineUnblockAngle && distanceToTarget < lineUnblockDistance)
            {
                if (debug) Debug.Log("ANGLE!! : " + angle);
                Vector3 force = directionToTarget / directionToTarget.magnitude * lineUnblockForce;
                motor.rigid.AddForce(force);
            }


        }

    }

    Vector3 FollowCursorFormation()
    {
        Vector3 position = new Vector3();


        if (controlMode == "MOUSE")
        {
            position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }

        if (controlMode == "X360")
        {
            float xAxis = Input.GetAxis("L_XAxis_1");
            float yAxis = -Input.GetAxis("L_YAxis_1");
            Vector3 dir = new Vector3(xAxis, yAxis, 0);

            if (dir.sqrMagnitude > 0.1f)
            {
                Vector3 dirNormal = dir.normalized;

                position = dirNormal * 100;
            }

            position += transform.position;

        }
        
        return position;
    }

    int lineFormationStrength = 20;

    Vector3 LineFormation ()
    {
        if (lineFollowing)
            return ((lineFollowing.position * lineFormationStrength)  + Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10))  ) / (lineFormationStrength + 1);
        return new Vector3();
    }
    
}
