using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Patrolling))]
public class Detection : MonoBehaviour
{
    [HeaderAttribute("The 'AHA! I've found you' sound" )]
    public AudioSource audioDetection;

    [HeaderAttribute("Minimal Horizontal Detectio Distance")]
    [RangeAttribute(3,20)]
    public float horizontalDetection=10f;
    
    [HeaderAttribute("Minimal Vertical Detection Distance")]
    [RangeAttribute(1,3)]
    public float verticalDetection = 2f;
    
    [HeaderAttribute("Forward Detection Angle")]
    [RangeAttribute(30,120)]
    public float detectionAngle = 90f;

    private bool detectionSequenceStarted = false;

    private string guardObjectName;

    //private Transform playerObjectTransform;
    private GameObject[] playersObjects;
    private LevelController levelController;
    private Transform localTransform;
    private Patrolling patrollingComponenet;
    private Vector3 guardPlayerRelativePosition;

    void Start()
    {
        guardObjectName = gameObject.name;
        playersObjects = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("This is number of player objects: " + playersObjects.Length);
        levelController = GameObject.FindGameObjectWithTag("MainControllers").GetComponent<LevelController>();
        patrollingComponenet = gameObject.GetComponent<Patrolling>();
        localTransform = transform;
    }

    public void NewPlayerHasSpawned()
    {
        playersObjects = GameObject.FindGameObjectsWithTag("Player");
    }

    void LateUpdate()
    {
        foreach (GameObject player in playersObjects)
        {
            if (CheckPlayerDetection(player.transform))
            {
                StartDetectionTimeout();
            }
            else
            {
                StopDetectionTimeout();
            }
        }
    }

    private bool CheckPlayerDetection(Transform playerObjectTransform)
    {

        bool detected = false;
        if (Mathf.Abs(localTransform.position.y - playerObjectTransform.position.y) < verticalDetection)
        {
            if (Vector3.Distance(localTransform.position, playerObjectTransform.position) < horizontalDetection)
            {
                Vector3 playerPositionDelta = playerObjectTransform.position - localTransform.position;
                if (Vector3.Angle(playerPositionDelta, transform.forward) < detectionAngle)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(localTransform.position, playerPositionDelta, out hit, horizontalDetection))
                    {
                        if (hit.collider.tag == "Player")
                        {
                            detected = true;
                        }
                    }
                }
            }
        }

        return detected;
    }

    private void StartDetectionTimeout()
    {
        if (!detectionSequenceStarted)
        {
            patrollingComponenet.FollowPlayerMovement();
            if (levelController)
            {
                levelController.StartCountdown();
            }
            detectionSequenceStarted = true;
        }
    }
    private void StopDetectionTimeout()
    {
        if (detectionSequenceStarted)
        {
            patrollingComponenet.StopFollowingPlayerMovement();
            if (levelController)
            {
                levelController.StopCountdown();
            }
            detectionSequenceStarted = false;
        }
    }

}
