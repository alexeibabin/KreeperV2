using UnityEngine;
using System.Collections;

public class WalkBehaviour : BaseBehaviour
{

    public override void Action(GameObject target)
    {
        Debug.Log("Trying to walk " + target);
        PlayerControll playerController = target.GetComponent<PlayerControll>();
        Quaternion targetRotation;

        GameObject waypointObject = GetParentWaypointObject().gameObject;
        WayPointController waypointController = waypointObject.GetComponent<WayPointController>();

        switch (waypointController.playerRotation)
        {
            case WayPointController.WaypointRotate.GlobalLeft:
                targetRotation = Quaternion.Euler(Vector3.left);
                break;
            case WayPointController.WaypointRotate.GlobalRight:
                targetRotation = Quaternion.Euler(Vector3.right);
                break;
            case WayPointController.WaypointRotate.PlayerForward:
                targetRotation = target.transform.rotation;
                break;
            case WayPointController.WaypointRotate.WaypointForward:
                targetRotation = waypointObject.transform.rotation;
                break;
            default:
                targetRotation = target.transform.rotation;
                break;
        }

        playerController.SetWalk();

        playerController.SetDestinationWaypoint(waypointObject, waypointObject.transform.position, targetRotation);
    }
}
