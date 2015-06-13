using UnityEngine;
using System;
using System.Collections;
//using System.Reflection;
using System.Collections.Generic;


public class Patrolling : MonoBehaviour
{
    [SerializeField]
    public List<Transform> waypointsList;

    [RangeAttribute(1, 5)]
    public float speed = 3f;

    [HeaderAttribute("Circle patroll")]
    [TooltipAttribute("Should AI patroll the waypoints in a circular matter")]
    public bool waypointsCycle = true;

    [HeaderAttribute("Waypoints Connections Color (for debugging)")]
    public Color waypointsDebugColor = Color.black;

    public bool doPatrol = true;
    private bool followingPlayerMovement = false;

    private Vector3 targetWaypoint;
    private Vector3 moveDirection;
    private Vector3 myVelocity;

    private int currentWayPointIndex = 0;

	private GameObject playerObject;
    private Transform playerObjectTransform;
    private Transform localTransform;
    private Rigidbody physicsComponenet;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        localTransform = transform;
        physicsComponenet = GetComponent<Rigidbody>();
		playerObject = GameObject.FindGameObjectWithTag("Player");
		if (playerObject) {
			playerObjectTransform = playerObject.transform;
		}
    }

	void FixedUpdate(){
		if (!playerObjectTransform) {
			playerObjectTransform = GameObject.FindGameObjectWithTag("Player").transform;
		}
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
                if (waypointsList[waypointsIndex - 1])
                {
                    Gizmos.DrawLine(waypointsList[waypointsIndex - 1].position, waypointsList[waypointsIndex].position);
                }
            }
            if (waypointsList[0])
            {
                Gizmos.DrawLine(waypointsList[0].position, lastWaypoint.position);
            }
        }
    }

    IEnumerator FollowThePlayer()
    {
        Vector3 tempVector3;
        Debug.Log("Following the player (coroutine)");
        while (true)
        {
            
            tempVector3 = playerObjectTransform.position;
            tempVector3.y = localTransform.position.y;
            localTransform.LookAt(tempVector3);
            yield return new WaitForFixedUpdate();
        }
    }

    public void FollowPlayerMovement()
    {
        if (!followingPlayerMovement)
        {
            Debug.Log("Following the player (public function)");
            StopPatrolling();
            StartCoroutine("FollowThePlayer");
            followingPlayerMovement = true;
        }
    }

    public void StopFollowingPlayerMovement()
    {
        if (followingPlayerMovement)
        {
            ContinuePatrolling();
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
        if (doPatrol)
        {
            if (currentWayPointIndex < waypointsList.Count && currentWayPointIndex > -1)
            {
                targetWaypoint = waypointsList[currentWayPointIndex].position;
                targetWaypoint.y = localTransform.position.y;
                moveDirection = targetWaypoint - localTransform.position;
                myVelocity = physicsComponenet.velocity;

                //the more closer we get to the way point the slower we go
                //             if (moveDirection.magnitude < 2)
                //             {
                //                 //myVelocity = moveDirection.normalized * (speed * 2 / 3);
                //                 //if (moveDirection.magnitude < 2)
                //                 //{
                //                 SetWalkingSpeed();
                //                 //myVelocity = moveDirection.normalized * (speed * 1 / 3);
                // 
                //                 //}
                //             }
                //             else
                //             {
                // 
                //                 //                 SetRunningSpeed();
                //                 //myVelocity = moveDirection.normalized * speed;
                //             }
                SetWalkingSpeed();
                if (moveDirection.magnitude < 1)
                {
                    if (waypointsCycle)
                        currentWayPointIndex++;
                    else
                        currentWayPointIndex--;
                }
            }

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
            SetIdle();
        }

        //set the rigid body velocity to current script velocity and make AI look at next waypoint
        physicsComponenet.velocity = myVelocity;

    }

    private void SetRunningSpeed()
    {
        myVelocity = moveDirection.normalized * speed;
        animator.SetFloat("HorizontalVelosity", myVelocity.magnitude);
    }

    private void SetWalkingSpeed()
    {
        myVelocity = moveDirection.normalized * (speed * 0.5f);
        animator.SetFloat("HorizontalVelosity", myVelocity.magnitude);
    }

    private void SetIdle()
    {
        myVelocity = Vector3.zero;
        animator.SetFloat("HorizontalVelosity", myVelocity.magnitude);
    }

    public void SetWaypoints(Transform[] waypoints)
    {
        if (waypointsList == null )
        {
            waypointsList = new List<Transform>();
        }
        waypointsList.AddRange(waypoints);
    }
}

