using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour
{


    public float walkSpeed = 5f;
    public float walkRotate = 5f;
    public float runSpeed = 7.5f;
    public float runRotate = 7.5f;
    public float rayCastDistance = 10f;

    private float movementSpeed;
    private float rotateSpeed;
    private GameObject targetWaypoint;
    private Vector3 targetWaypointTransform;
    private Quaternion targetWaypointRotation;

    private CardboardHead head;

    private GameObject lookedAtWaypoint;


    // Use this for initialization
    void Start()
    {
        targetWaypoint = null;
        head = Camera.main.GetComponent<StereoController>().Head;

        SetWalk();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
        RotateToWaypoint();
    }

    void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(head.Gaze.origin, head.Gaze.direction);
        
        if (Physics.Raycast(head.Gaze, out hit, rayCastDistance))
        {
            if (hit.collider.tag == "WayPoint")
            {
                Debug.Log("Looking at waypoint");
                lookedAtWaypoint = hit.collider.gameObject;
                lookedAtWaypoint.GetComponent<WayPointController>().Activate();
            }
            else
            {
                if (lookedAtWaypoint)
                {
                    lookedAtWaypoint.GetComponent<WayPointController>().Deactivate();
                }
            }
        }
        //bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, rayCastDistance);

    }

    public void SetDestinationWaypoint(GameObject target, Vector3 targetTransform, Quaternion targetRotation)
    {
        if (targetWaypoint)
        {
            // Default Layer
            targetWaypoint.layer = 0;
            targetWaypoint.SetActive(true);

        }
        // Set the new target waypoint
        targetWaypoint = target;

        targetWaypointRotation = targetRotation;
        targetWaypointTransform = targetTransform;
        targetWaypoint.SetActive(false);

        // Ignore Raycast Layer
        targetWaypoint.layer = 2;

    }

    public void SetRun()
    {
        movementSpeed = runSpeed;
        rotateSpeed = runRotate;
    }

    public void SetWalk()
    {
        movementSpeed = walkSpeed;
        rotateSpeed = walkRotate;
    }



    void MoveToWaypoint()
    {

        if (targetWaypoint)
        {
            Vector3 deformedPosition;
            if (Vector3.Distance(transform.position, targetWaypointTransform) < 0.2f)
            {
                deformedPosition = targetWaypointTransform;
            }
            else
            {
                deformedPosition = Vector3.Lerp(transform.position, targetWaypointTransform, movementSpeed * Time.deltaTime);
                //deformedPosition.y = transform.position.y;
                transform.position = deformedPosition;
            }
            targetWaypoint.SetActive(false);
        }
    }

    void RotateToWaypoint()
    {
        if (targetWaypoint)
        {
            Quaternion tempRotation = Quaternion.Lerp(transform.rotation, targetWaypointRotation, rotateSpeed * Time.deltaTime);
            Vector3 eulerAngles = tempRotation.eulerAngles;
            eulerAngles.x = transform.rotation.eulerAngles.x;
            eulerAngles.z = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }
}
