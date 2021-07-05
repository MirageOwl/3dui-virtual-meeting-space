using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    public Vector3 oldPosition;
    public Vector3 newPosition;
    bool walkPointSet;
    public float walkPointRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        oldPosition = agent.transform.position;
        newPosition = oldPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.updateRotation = false;
        newPosition = agent.transform.position;

        if (oldPosition == newPosition)
            walkPointSet = false;

        Patroling();

        /*
        Vector3 movementDirection = agent.velocity.normalized;
        if (movementDirection != Vector3.zero)
        {
            //agent.transform.forward = movementDirection;
       
            agent.updateRotation = true;
        }*/
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        oldPosition = agent.transform.position;

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

}
