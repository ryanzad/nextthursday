using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMotor : MonoBehaviour {

    public MasterReferences master;



    [Header("REFERENCES")]
    public Rigidbody2D rigid;
    public TargetHandler targetHandler;



    [Header("CONTROLS")]

    [Tooltip("speed to move player")]
    public float forwardInitSpeed = 100f;

    [Tooltip("how much the target distance influences player speed (higher = less influence)")]
    public float targetDistanceSpeed = 1;

    [Tooltip("limits how close the player can get to the target")]
    public float targetDistanceThreshold = 0.6f;

    [Range(0,1)] [Tooltip("how slow the player gets when they approach the target (lower = slower)")]
    public float targetDistanceThresholdDamping = 0.1f;

    [Tooltip("x = min, y = max, it'll select a random speed between those two")]
    public Vector2 turnSpeedRange;

    [Tooltip("how long it takes to fire up the engines after it's off")]
    public float turnOnDelay;

    Vector3 target;
    [HideInInspector] public float turnSpeed;
    float turnSpeedInit, turnSpeedLineFormation;


    public bool active = false;
    public bool allowDeath = true;

    public float explodeRadius;
    //1 = normal size, 2 = 2x size


    int hitState = 0;
    





    public void On ()
    {
        StopAllCoroutines();
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn ()
    {
        yield return new WaitForSeconds(turnOnDelay);
        active = true;
    }








    private void Start()
    {
        turnSpeed = Random.Range(turnSpeedRange.x, turnSpeedRange.y);
        turnSpeedInit = turnSpeed;
        turnSpeedLineFormation = turnSpeed * 10f;

    }

    

    void Update () {
        
        if (active)
        {
            

            target = targetHandler.GetTarget();


            LookAtTarget();
            
                if (GetTargetDistance() > targetDistanceThreshold)
                {
                    rigid.AddForce(transform.right * forwardInitSpeed * (GetTargetDistance() + targetDistanceSpeed));

                }
                else
                {
                    rigid.velocity *= targetDistanceThresholdDamping;
                }
            TargetHandlerMechanic();
        }
        
        
    }

    public void DieAlly ()
    {
        if (!allowDeath) return;
        allowDeath = true;
        tag = "Dead";

        master.scorer.KillAlly();

        StartCoroutine(Explode());
    }

    public void DieNPC ()
    {
        if (!allowDeath) return;
        allowDeath = true;
        tag = "Dead";

        StartCoroutine(Explode());
    }

    public IEnumerator Explode ()
    {
         foreach (Transform child in transform)
         {
             child.GetComponent<Renderer>().enabled = false;
         }

         GetComponent<BoxCollider2D>().size *= explodeRadius;
         yield return new WaitForSeconds(0.1f);
         Destroy(gameObject);
    }


    void LookAtTarget()
    {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg),
                turnSpeed * Time.deltaTime);

    }
    
    void TargetHandlerMechanic ()
    {
        switch (targetHandler.formation)
        {
            case TargetHandler.FormationMode.LINE:
                turnSpeed = turnSpeedLineFormation;
                break;
            case TargetHandler.FormationMode.FOLLOW_CURSOR:
                turnSpeed = turnSpeedInit;
                break;

        }

    }

    float GetTargetDistance()
    {
        return Vector2.Distance(target, transform.position);
    }


    
}
