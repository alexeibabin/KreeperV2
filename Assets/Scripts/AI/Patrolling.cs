using UnityEngine;
using System;
using System.Collections;
//using System.Reflection;
using System.Collections.Generic;


public class Patrolling : MonoBehaviour
{
    [SerializeField]
    public List<Transform> waypointsList;
    
    [RangeAttribute(1,5)]
    public float speed = 3f;

    [HeaderAttribute("Circle patroll")]
    [TooltipAttribute("Should AI patroll the waypoints in a circular matter")]
    public  bool waypointsCycle = true;
    
    [HeaderAttribute("Waypoints Connections Color (for debugging)")]
    public Color waypointsDebugColor = Color.black;

    private bool doPatrol = true;
    private bool followingPlayerMovement = false;

    private Vector3 targetWaypoint;
    private Vector3 moveDirection;
    private Vector3 myVelocity;

    private int currentWayPointIndex = 0;

    private Transform playerObjectTransform;
    private Transform localTransform;
    private Rigidbody physicsComponenet;

    void Start()
    {
        localTransform = transform;
        physicsComponenet = GetComponent<Rigidbody>();
        playerObjectTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnDrawGizmos()
    {
        if (waypointsList != null && waypointsList.Count > 0)
        {
            //Debug.Log(waypointsList);
            Gizmos.color = waypointsDebugColor;
            Transform lastWaypoint = waypointsList[waypointsList.Count - 1];
            for (int waypointsIndex = 1; waypointsIndex < waypointsList.Count; waypointsIndex++)
            {
                Gizmos.DrawLine(waypointsList[waypointsIndex - 1].position, waypointsList[waypointsIndex].position);
            }
            Gizmos.DrawLine(waypointsList[0].position, lastWaypoint.position);
        }
    }

    IEnumerator FollowThePlayer()
    {
        Vector3 tempVector3;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            tempVector3 = playerObjectTransform.position;
            tempVector3.y = localTransform.position.y;
            localTransform.LookAt(tempVector3);
        }
    }

    public void FollowPlayerMovement()
    {
        if (!followingPlayerMovement)
        {
            StartCoroutine("FollowThePlayer");
            followingPlayerMovement = true;
        }
    }

    public void StopFollowingPlayerMovement()
    {
        if (followingPlayerMovement)
        {
            StopCoroutine("FollowThePlayer");
            followingPlayerMovement = false;
        }
    }

    public void StopPatrolling()
    {
        doPatrol = false;
    }

    public void ContinuePatrolling()
    {
        doPatrol = true;
    }

    void Update()
    {
        // if the waypoint is in the array
        if (currentWayPointIndex < waypointsList.Count && currentWayPointIndex > -1)
        {
            //targetWaypoint = wayPoints[currentWayPointIndex].position;
            targetWaypoint = waypointsList[currentWayPointIndex].position;
            targetWaypoint.y = localTransform.position.y;
            moveDirection = targetWaypoint - localTransform.position;
            myVelocity = physicsComponenet.velocity;

            //the more closer we get to the way point the slower we go
            if (moveDirection.magnitude < 4)
            {
                myVelocity = moveDirection.normalized * (speed * 2 / 3);
                if (moveDirection.magnitude < 2)
                {
                    myVelocity = moveDirection.normalized * (speed * 1 / 3);
                    if (moveDirection.magnitude < 1)
                    {
                        if (waypointsCycle)
                            currentWayPointIndex++;
                        else
                            currentWayPointIndex--;
                    }
                }
            }
            else
            {
                myVelocity = moveDirection.normalized * speed;
            }
        }
        if (doPatrol)
        {
            if (currentWayPointIndex >= waypointsList.Count)
            {
                waypointsCycle = false;
                currentWayPointIndex = waypointsList.Count - 1;
            }
            if (currentWayPointIndex <= 0)
            {
                waypointsCycle = true;
                currentWayPointIndex = 0;
            }
            localTransform.LookAt(targetWaypoint);
        }
        else
        {
            myVelocity = Vector3.zero;
        }

        //set the rigid body velocity to current script velocity and make AI look at next waypoint
        physicsComponenet.velocity = myVelocity;

    }
}

