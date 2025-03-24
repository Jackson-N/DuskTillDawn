using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BearScript : MonoBehaviour
{
    //Main script that controlls the bear's actions
    //actions including: Idle, Stalk, Follow, Attack, Flee, and Kill

    /*
    Idle: Not moving
    Stalk: Follows player from a set distance. the bear will not attack the player but will wonder around the player
    Follow: Follows from a closer distance than stalk
    Attack: runs towards player and readies to kill
    Flee: runs away from player based on light intensity from LightDetection.cs
    Kill: kills player, and sends player to game over
    */

    //import scripts from LightDetection.cs and SanityController.cs
    public LightDetection lightDetection;
    public SanityController sanity;
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    [SerializeField] private bool isTouchingGround;
    bool canSetWalkPoint;
    bool walkPointSet;
    public float walkPointRange;
    [SerializeField] private float walkSpeed = 40.0f;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    [SerializeField] private bool isStuck;
    private bool isInBounds;

    //public variables
    private float followDistanceMax;
    private float followDistanceMin;
    private float fleeDistance;
    private float attackDistance = 30.0f;
    private float killDistance;
    private float sanityCheck;

    private float time = 0.0f;
    public bool gameOver = false;
    [SerializeField] public GameObject target;

    public enum bearState
    {
        bearIdle,
        bearStalk,
        bearFollow,
        bearAttack,
        bearFlee,
        bearKill
    }

    public bearState currentState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = this.gameObject;
    }



    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        followDistanceMax = 100.0f;
        followDistanceMin = 75.0f;
        attackDistance = 30.0f;
        fleeDistance = 100.0f;
        killDistance = 5.0f;
        //sanity = GetComponent<SanityController>();
        bearIdle();
    }

    // Update is called once per frame
    void Update()
    {
        if(bearIdle())
        {
            checkState();
            checkGround();
            checkBounds();
            checkIsStuck();
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if(!playerInSightRange && !playerInAttackRange) currentState = bearState.bearStalk;
            if(playerInSightRange && !playerInAttackRange) currentState = bearState.bearFollow;
            if(playerInSightRange && playerInAttackRange) currentState = bearState.bearAttack;
            if(currentState == bearState.bearStalk && lightDetection.lightIntensity <= followDistanceMax) currentState = bearState.bearFollow;
            if(currentState == bearState.bearFollow && lightDetection.lightIntensity <= followDistanceMin) currentState = bearState.bearStalk;
            if(currentState == bearState.bearFollow && lightDetection.lightIntensity == player.transform.position.magnitude) currentState = bearState.bearFlee;
            if(currentState == bearState.bearAttack && lightDetection.lightIntensity <= fleeDistance) currentState = bearState.bearFlee;
            if(currentState == bearState.bearFlee && (lightDetection.lightIntensity >= followDistanceMin || Vector3.Distance(transform.position, player.transform.position) >= 100.0f)) currentState = bearState.bearStalk;
            if(currentState == bearState.bearAttack && lightDetection.lightIntensity >= killDistance) currentState = bearState.bearKill;

            //Debug.Log("Distance from player: " + Vector3.Distance(transform.position, player.transform.position));
            //Debug.Log("Light Intensity: " + lightDetection.lightIntensity);
        }
    }

    void checkState()
    {
        //check the current state of the bear
        switch(currentState)
        {
            case bearState.bearIdle:
                bearIdle();
                break;
            case bearState.bearStalk:
                bearStalk();
                break;
            case bearState.bearFollow:
                bearFollow();
                break;
            case bearState.bearAttack:
                bearAttack();
                break;
            case bearState.bearFlee:
                bearFlee();
                break;
            case bearState.bearKill:
                bearKill();
                break;
        }
    }

    void checkGround()
    {
        //check if the bear is touching the ground
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 1.0f, whatIsGround))
        {
            isTouchingGround = true;
        }
        else isTouchingGround = false;
    }

    void checkIsStuck()
    {
        //check if the bear is stuck
        if(agent.speed == 0.0f)
        {
            //set isStuck to true and move to a new walkpoint
            isStuck = true;
            bearStalk();
        }
        else isStuck = false;
    }

    void checkBounds()
    {
        //check if walkpoint is within the bounds of the map NavMesh
        //if the walkpoint is outside the bounds, set walkpoint to a new point
        if(agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            isInBounds = false;
            findWalkPoint();
        }
        else isInBounds = true;
        
    }

    bool bearIdle()
    {
        time += Time.deltaTime;
        bool isIdle = true;
        //Debug.Log(time);

        if(time >= 5.0f)
        {
            //Debug.Log("Bear is now stalking player");
            currentState = bearState.bearStalk;
        }
        return isIdle;
    }

    void bearStalk()
    {
        //bear follows player from a set distance. the bear can move as closer or as far from the player as the min and max values allow
        //if the lightdection is lower than max distance value, the bear can move closer to the player, if not, then move further away.
        //the bear cannot move closer than the min distance value, and cannot move further than the max distance value
        
        //the bear should NOT move in a straight path towards the player, but should move in a random path around the player
        //the bear should also move in a random speed, but should not exceed the max speed value

        //if the light intensity is lower than the max distance value, the bear can move closer to the player

        if (!walkPointSet) findWalkPoint();
    
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        Vector3 distanceToPlayer = transform.position - player.transform.position;
        //set walk speed to 15.0f
        agent.speed = walkSpeed;

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            if(distanceToWalkPoint.magnitude > 1f && !isStuck && isInBounds)
            {
                agent.SetDestination(walkPoint);
            }
            else if(distanceToWalkPoint.magnitude <= 1f && !isStuck && isInBounds)
            {
                walkPointSet = true;
            }
        }
        walkPointSet = false;
        canSetWalkPoint = true;
    }

    private void findWalkPoint()
    {
        //pick a random point and walk to it
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);


        //set walkpoint to the values given by randomX and randomZ
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //check if the walkpoint is on the ground
        if (canSetWalkPoint)
        {
            //Debug.Log("Bear is grounded");
            if (!Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) return;
            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                //Debug.Log("Walkpoint found");
                walkPointSet = true;
            }
        }
    }

    void bearFollow()
    {
        //bear follows player from a closer distance
        //move to player position, but only as close as the follow distance value allows

        //if the light intensity is lower than the max distance value, the bear can move closer to the player
        Vector3 distanceToPlayer = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer.normalized;

        if(distanceToPlayer.magnitude <= followDistanceMin)
        {
            agent.SetDestination(transform.position - distanceToPlayer);
        }
        else if(distanceToPlayer.magnitude >= followDistanceMax)
        {
            agent.SetDestination(transform.position + distanceToPlayer);
        }
        else agent.SetDestination(player.transform.position);

        //TODO: add sanity variable later

    }

    void bearAttack()
    {
        agent.SetDestination(player.transform.position);
    }

    void bearFlee()
    {
        //bear runs away from player based on light intensity from LightDetection.cs
        //if the light intensity is higher than the flee distance value, the bear will run away from the player
        //sanityCheck = sanity.currentSanity;
        Debug.Log("Sanity: " + sanityCheck);
        sanityCheck = sanity.currentSanity;

        Vector3 distanceToPlayer = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer.normalized;

        if(lightDetection.lightIntensity >= fleeDistance || sanityCheck <= 30.0f)
        {
            //go to flee distance
            agent.SetDestination(transform.position - distanceToPlayer);    //NOTE: this works
            sanity.currentRate--;
            //wait for 3 seconds
            
            //agent.SetDestination(transform.position + distanceToPlayer);
        }
        else
        {
            agent.SetDestination(player.transform.position);
            sanity.currentRate++;
        }

        //add sanity variable later
    }

    void bearKill()
    {
        //bear is touching player, gameOver = true
        if (transform.position == player.transform.position) gameOver = true;
        else agent.SetDestination(player.transform.position);
    }
}
