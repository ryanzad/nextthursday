using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMotor : MonoBehaviour {

    public MasterReferences master;

    [Header("REFERENCES")]
    public Rigidbody2D rigid;

    [Header("CONTROLS")]

    [Tooltip("speed to move player")]
    public float forwardInitSpeed = 100f;

    [Tooltip("how much mouse distance influences player speed (higher = less influence)")]
    public float mouseDistanceSpeed = 1;

    [Tooltip("limits how close the player can get to the mouse")]
    public float mouseDistanceThreshold = 0.6f;

    [Range(0,1)] [Tooltip("how slow the player gets when they approach the mouse (lower = slower)")]
    public float mouseDistanceThresholdDamping = 0.1f;

    [Tooltip("x = min, y = max, it'll select a random speed between those two")]
    public Vector2 turnSpeedRange;

    [Tooltip("how long it takes to fire up the engines after it's off")]
    public float turnOnDelay;

    Vector3 mouseScreen, mouse;
    float turnSpeed;


    public bool active = false;
    public bool allowDeath = true;



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
    }

    

    void Update () {
        
        if (active)
        {

            mouseScreen = Input.mousePosition;
            mouse = Camera.main.ScreenToWorldPoint(mouseScreen);

            MouseRotation();


            if (Input.GetMouseButton(0))
            {
                if (GetMouseDistance() > mouseDistanceThreshold)
                {
                    rigid.AddForce(transform.right * forwardInitSpeed * (GetMouseDistance() + mouseDistanceSpeed));

                }
                else
                {
                    rigid.velocity *= mouseDistanceThresholdDamping;
                }
            }

            /*
            if (Input.GetMouseButtonDown(1) && allowDeath)
            {
                if (Random.Range(1, 10) == 5)
                {
                    StartCoroutine(Explode());
                }
            }*/
        }

        
        



/*
            if (Input.GetKey(KeyCode.W))
            {
                rigid.AddForce(transform.forward * forwardInitSpeed * 10);
            }



            if (Input.GetKey(KeyCode.A))
            {
                rigid.AddTorque(new Vector3(0, 0, turnSpeed));
                rigid.AddForce(transform.forward * forwardInitSpeed * turnPushStrength * 10);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rigid.AddTorque(new Vector3(0, 0, -turnSpeed));
                rigid.AddForce(transform.forward * forwardInitSpeed * turnPushStrength * 10);
            }*/
        
    }

    public void DieAlly ()
    {
        if (!allowDeath) return;
        allowDeath = true;

        master.scorer.KillAlly();

        StartCoroutine(Explode());
    }

    public void DieNPC ()
    {
        if (!allowDeath) return;
        allowDeath = true;
        
        StartCoroutine(Explode());
    }

    public IEnumerator Explode ()
    {
        /* foreach (Transform child in transform)
         {
             child.GetComponent<Renderer>().enabled = false;
         }

         GetComponent<BoxCollider2D>().size *= 4f;
         yield return new WaitForSeconds(0.1f);*/
        yield return null;
        Destroy(gameObject);
    }


    void MouseRotation()
    {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg),
                turnSpeed * Time.deltaTime);
        /*
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation,
            Quaternion.Euler(Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * -Mathf.Rad2Deg, 90, 90),
            turnSpeed * Time.deltaTime);*/

    }

    float GetMouseDistance ()
    {
        return Vector2.Distance(mouse, transform.position);
    }
    
}
