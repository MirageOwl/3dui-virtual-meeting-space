using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// S.R In progress, different method
// Use AIMovement Script Instead

public class AltAIMovement : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
    }


    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f,70f);
    }


    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
    }


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;



    /*
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    */


}
