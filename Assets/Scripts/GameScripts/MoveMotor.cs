using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMotor : MonoBehaviour {

    [Header("REFERENCES")]
    public Rigidbody rigid;

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

    Vector3 mouseScreen, mouse;
    float turnSpeed;


    public bool active = false;
    public bool allowDeath = true;



    int hitState = 0;
    

    private void Start()
    {
        turnSpeed = Random.Range(turnSpeedRange.x, turnSpeedRange.y);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (!active && collision.gameObject.name.Contains("player")) {
            switch (hitState) {
                case 0:
                    hitState = 1;
                    transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                   // transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    break;
                case 1:
                    hitState = 2;
                    transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 2:
                    hitState = 3;
                    transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
                    active = true;
                    break;
            }
        }
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
                    rigid.AddForce(transform.forward * forwardInitSpeed * (GetMouseDistance() + mouseDistanceSpeed));

                }
                else
                {
                    rigid.velocity *= mouseDistanceThresholdDamping;
                }
            }


            if (Input.GetMouseButtonDown(1) && allowDeath)
            {
                if (Random.Range(1, 10) == 5)
                {
                    StartCoroutine(Explode());
                }
            }
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

    IEnumerator Explode ()
    {
       // transform.localScale *= 4f;
        GetComponent<BoxCollider>().size *= 4f;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }


    void MouseRotation()
    {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * -Mathf.Rad2Deg, 90, 90),
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
