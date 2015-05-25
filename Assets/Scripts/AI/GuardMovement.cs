using UnityEngine;
using System.Collections;

public class GuardMovement : MonoBehaviour {

    public GuardWaypoint[] routeWaypoints;
    public float moveSpeed;

    int waypointIndex;
    PlayerControll criminal;

    GuardState state;
    enum GuardState
    {
        Idle,
        Moving,
        Chasing
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case GuardState.Idle:
                SetNextWaypoint();
                break;
            case GuardState.Moving:
                MoveToWaypoint();
                break;
            case GuardState.Chasing:
                ChaseCriminal();
                break;
            default:
                break;
        }
	}

    private void ChaseCriminal()
    {
        throw new System.NotImplementedException();
    }

    private void MoveToWaypoint()
    {
        //  Move towards waypoint
    }

    private void SetNextWaypoint()
    {
        waypointIndex = waypointIndex + 1 >= routeWaypoints.Length ? 0 : waypointIndex + 1;
    }
}
