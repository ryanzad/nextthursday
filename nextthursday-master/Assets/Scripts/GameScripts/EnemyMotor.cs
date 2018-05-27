using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotor : MonoBehaviour {

    [HideInInspector] public MasterReferences master;

    [Header("REFERENCES")]
    public GameObject ProjectilePrefab;
    public Rigidbody rigid;

    [Header("Stand Back")]

    public bool standBack;
    public float standBackDistance;
    public float standBackSpeed;
    
    [Header("Look at Target")]
    public bool lookatTarget;
    public float lookatTargetSpeed;
    public float scanTargetDist;
    Collider foundTarget;


    [Header("Patrol")]
    public bool patrol;
    float patrolCounter = 0, patrolCounterMax;
    public Vector2 patrolMoveTime, patrolRotateTime;
    public float patrolForwardSpeed;
    public float patrolSideDistance;
    public float patrolSideSpeed;
    public float patrolRotateSpeed;
    public float patrolRotateChange; //how fast the rotate perlin changes
    public float patrolRotateChangeStrength; //how strong the rotate perlin is
    float patrolRotateRandom = 1;
    float patrolDir;
    enum PatrolMode { START, MOVE, ROTATE };
    PatrolMode patrolMode = PatrolMode.START;

    [Header("Check Collision")]
    public bool checkCollision;
    public float collisionDistanceNoTarget;
    public float collisionDistanceFoundTarget;
    float chooseCollisionDir; //which direction to move around when facing a wall

    [Header("Follow Target")]
    public bool followTarget;
    public float followSpeed;
    public float followDist;

    [Header("Drift")]
    public bool drift;
    public float driftInstability;
    public float driftStrength;

    [Header("Shoot")]
    public bool shoot;
    public float shootInterval;
    public float shootDistanceMin, shootDistanceMax;
    bool allowShoot;
    float shootCount = 0;
    public float bulletSpeedMulti = 1;
    public float bulletIntervalMulti = 1;

    [Header("Check Shoot")]
    public bool checkShoot;
    public float collisionDistanceNoShoot; //the closest the enemy can get to the player before it stops shooting. This is for the trample mechanic (so enemies don't keep shooting as the player attempts to trample them).
    public float noShootVelocity; //the velocity when the enemy stops shooting (because it's moving quickly)


    [Header("Mod Effects")]
    public PhysicMaterial bouncyPhysics;

    float randomSeed;

    public bool debug;

    private void Start()
    {
        randomSeed = Random.Range(0, 1000);


        foreach (Modifiers.Modifier mod in master.modifiers.mods)
        {
            ModSettings_Start(mod);
        }
    }


    public Collider GetFoundTarget ()
    {
        return foundTarget;
    }

    void ModSettings_Start(Modifiers.Modifier mod)
    {
        switch (mod)
        {
            case Modifiers.Modifier.ANGRY:
                Mod_Angry();
                break;
            case Modifiers.Modifier.FASTER:
                Mod_Faster();
                break;

            case Modifiers.Modifier.PUNISHING:
                Mod_Punishing();
                break;

            case Modifiers.Modifier.SLIPPERY:
                Mod_Slippery();
                break;


            case Modifiers.Modifier.BOUNCY:
                Mod_Bouncy();
                break;

            case Modifiers.Modifier.BIGGER:
                Mod_Bigger();
                break;
        }
    }

    void Mod_Punishing()
    {
        scanTargetDist = 1000;
        bulletSpeedMulti *= 3;
        shootDistanceMax = 99;
        shootDistanceMin = 0;
    }


    void Mod_Bigger()
    {
        transform.localScale *= Random.Range(0.3f, 0.6f);
        rigid.mass *= 2f;
    }

    void Mod_Bouncy()
    {
        GetComponent<BoxCollider>().sharedMaterial = bouncyPhysics;
    }


    void Mod_Slippery()
    {
        GetComponent<Rigidbody>().drag *= 0.05f;
        GetComponent<Rigidbody>().angularDrag *= 0.5f;
        shootDistanceMax = 99;
    }

    void Mod_Angry()
    {
        patrolMoveTime.x = 0;
        patrolMoveTime.y = 0.5f;
        patrolRotateTime.x = 0;
        patrolRotateTime.y = 0.5f;
        patrolForwardSpeed *= 1.3f;
        patrolSideDistance *= 10f;
        patrolRotateSpeed *= 7;
        driftInstability *= 10;
        driftStrength *= 7.5f;
        shootDistanceMax = 99;
        shootDistanceMin = 0;
     //   shootInterval *= 0.3f;
    }

    void Mod_Faster ()
    {
        followSpeed *= 10f;
        lookatTargetSpeed *= 10f;
    }



    void Update () {
        if (standBack) StandBack();
        if (lookatTarget) LookAtTarget();
        if (patrol && !foundTarget) Patrol(); //if you havent found a target, patrol
        if (checkCollision) CheckCollision();
        if (followTarget && foundTarget) FollowTarget(); //if you have found a target, follow it
        if (shoot && foundTarget && allowShoot) Shoot();
        if (checkShoot) CheckShoot();
        if (drift) Drift();


        foreach (Modifiers.Modifier mod in master.modifiers.mods)
        {
            ModSettings_Update(mod);
        }
    }
    

    void ModSettings_Update(Modifiers.Modifier mod)
    {
        switch (mod)
        {
            case Modifiers.Modifier.BOUNCY:
                Mod_Bouncy_Update();
                break;
        }
    }



    void Mod_Bouncy_Update()
    {
        if (Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y) > 40)
        {
            rigid.velocity *= 0.8f;
        }
    }


    void StandBack ()
    {
        Collider2D[] nearbies = Physics2D.OverlapCircleAll(transform.position, standBackDistance, 1 << LayerMask.NameToLayer("Goodies"));
        foreach (Collider2D nearby in nearbies)
        {
            if (nearby.gameObject.tag == "Ally" || nearby.gameObject.tag == "Player")
            {
                rigid.AddForce(  (-(nearby.transform.position - transform.position) / nearbies.Length) * standBackSpeed);
               // Debug.Log("force: " + ((-(nearby.transform.position - transform.position) / nearbies.Length) * standBackSpeed)  + " " + nearbies.Length);
            }
        }
    }

    void LookAtTarget ()
    {
        SearchForNearestTarget(); //search for nearest target
        if (foundTarget)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, Mathf.Atan2(foundTarget.transform.position.y - transform.position.y, foundTarget.transform.position.x - transform.position.x) * Mathf.Rad2Deg),
                lookatTargetSpeed * Time.deltaTime);
        }
    }

    void SearchForNearestTarget()
    {
        //initialise variables
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        List<GameObject> neighbours = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
        neighbours.Add(GameObject.FindGameObjectWithTag("Player"));

        foreach (GameObject neighbour in neighbours)
        {
            if (transform && neighbour)
            {
                float distance = Vector3.Distance(transform.position, neighbour.transform.position);
                bool isNotMe = neighbour != this.gameObject;

                if (distance < minDist && isNotMe && distance < scanTargetDist)
                {
                    closest = neighbour;
                    minDist = distance;
                }
            }
        }
        
        if (closest)
        {
            foundTarget = closest.GetComponent<BoxCollider>();
        }







        /*
        Collider2D findTarget = Physics2D.OverlapCircle(transform.position, standBackDistance + 2, 1 << LayerMask.NameToLayer("Goodies"));
        if (findTarget)
        {
            if (findTarget.gameObject.tag == "Player" || findTarget.gameObject.tag == "Ally") 
            {
                foundTarget = findTarget;
            }
            */

        /*

        if (findTarget.gameObject.tag == "NPC") //target non con ONLY if it is seen on the screen.
        {                                       //that way enemies won't destroy all the non con at the start of the round
           // NPCHandler npcHandler = findTarget.gameObject.GetComponent<NPCHandler>();
              //  if (npcHandler.GetVisibility())
               // {
                    foundTarget = findTarget;
             //   }
        }
        */

        //}
    }


    void Patrol ()
    {
        PatrolSwitchRoles();

        switch (patrolMode)
        {
            case PatrolMode.MOVE:
                PatrolMove(1);
                break;
            case PatrolMode.ROTATE:
                PatrolRotate();
                PatrolMove(0.5f);
                break;
        }
       // patrolMode
       
    }

    void PatrolSwitchRoles ()
    {
        patrolCounter += Time.deltaTime;
        
        if (patrolCounter > patrolCounterMax)
        {
            patrolCounter = 0;
            switch (patrolMode)
            {
                case PatrolMode.START:
                    PatrolChangeToMove();
                    break;
                case PatrolMode.MOVE:
                    PatrolChangeToRotate();
                    break;
                case PatrolMode.ROTATE:
                    PatrolChangeToMove();
                    break;
            }
        }
        
    }

    void PatrolChangeToMove ()
    {
        patrolMode = PatrolMode.MOVE;
        patrolCounterMax = Random.Range(patrolMoveTime.x, patrolMoveTime.y);
    }

    void PatrolChangeToRotate ()
    {
        patrolMode = PatrolMode.ROTATE;
        patrolRotateRandom = Random.Range(0.75f, 1.25f);
        patrolDir = Random.Range(0, 2) == 1 ? -1 : 1;
        patrolCounterMax = Random.Range(patrolRotateTime.x, patrolRotateTime.y);
    }

    void CheckCollision () //check if a wall is in front
    {
        int layerMask = 1 << LayerMask.NameToLayer("Baddies");
        layerMask = ~layerMask;

        bool hitNoTarget = Physics.Raycast(transform.position, transform.right, collisionDistanceNoTarget, layerMask);
        bool hitFoundTarget = Physics.Raycast(transform.position,transform.right, collisionDistanceFoundTarget, layerMask);


        if (!foundTarget && hitNoTarget) //face opposite wall if you hit something
        {
           PatrolMove(-1f);
           transform.RotateAround(Vector3.forward, Mathf.PI); 
        }

        if (foundTarget && hitFoundTarget)
        {
            PatrolMove(-1f);
            rigid.AddForce(transform.up * 100 * chooseCollisionDir);

        }
        else
        {
            float dirSeed = (Time.time + (randomSeed / 10)) % 100 / 100;
            chooseCollisionDir = (dirSeed * 2) - 1;
        }


    }

    void CheckShoot ()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Baddies");
        layerMask = ~layerMask;
        bool hitNoShoot = Physics.Raycast(transform.position, transform.right, collisionDistanceNoShoot, layerMask);

        float enemyMoveVelocity = Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y);

        bool inDist = true;

        if (foundTarget) //if you've found a target, then shoot if in distance
        {
            float dist = Vector3.Distance(transform.position, foundTarget.transform.position);
            inDist = dist > shootDistanceMin && dist < shootDistanceMax;
        }


        allowShoot = !hitNoShoot && enemyMoveVelocity < noShootVelocity && inDist;
        if (debug)
        {
            Debug.Log(enemyMoveVelocity + " velocity");
        }

    }



    void PatrolMove(float forward)
    {
        rigid.AddForce(forward * transform.right * patrolForwardSpeed); //move forward
        rigid.AddForce(transform.up * PerlinValue(Time.time + randomSeed, patrolSideSpeed) * patrolSideDistance); //and move side to side
    //    Debug.Log(PerlinValue(Time.time + randomSeed, patrolSideSpeed));
    }

    void PatrolRotate ()
    {
        float perlin = PerlinValueSimple(Time.time + randomSeed, patrolRotateChange) * patrolRotateChangeStrength;
        perlin -= (perlin / 3f); //makes it go back and forth between positive and negative values (more on the positive side)

        rigid.AddTorque(new Vector3(0, perlin * patrolRotateSpeed * patrolRotateRandom * patrolDir, 0));
    }


    void FollowTarget ()
    {
        if (Vector3.Distance(transform.position, foundTarget.transform.position) > followDist)
        {
            rigid.AddForce(transform.right * followSpeed * 10f);
        }

    }

    void Shoot ()
    {
        shootCount += Time.deltaTime;

        if (shootCount >= shootInterval * bulletIntervalMulti)
        {
            shootCount = 0;
            FireProjectile();
        }
    }

    void FireProjectile ()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, transform);
        projectile.transform.localPosition = new Vector3(1, 0, 0);
        projectile.transform.parent = transform.parent;
        projectile.GetComponent<ProjectileCollision>().master = master;

        projectile.GetComponent<ProjectileMotor>().speedMulti = bulletSpeedMulti;
    }


    void Drift ()
    {
        Vector2 randomDriftSeed = new Vector2(randomSeed * 3, randomSeed * 8);
        
        rigid.AddForce(new Vector2(PerlinValue(Time.time + randomDriftSeed.x, driftInstability) * driftStrength,
            PerlinValue(Time.time + randomDriftSeed.y, driftInstability) * driftStrength  ));
    }



    // HELPER
    float PerlinValue (float time, float speed)
    {
        return (Mathf.PerlinNoise(time * speed / 10f, 0) * 2) - 1;
    }

    float PerlinValueSimple (float time, float speed)
    {
        return Mathf.PerlinNoise(time * speed / 10f, 0);
    }

}
