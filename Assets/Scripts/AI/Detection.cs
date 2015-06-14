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
    [RangeAttribute(1,20)]
    public float verticalDetection = 2f;
    
    [HeaderAttribute("Forward Detection Angle")]
    [RangeAttribute(30,120)]
    public float detectionAngle = 90f;

	public Transform eyes;

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

	void FixedUpdate(){
		if (playersObjects.Length == 0) {
			playersObjects = GameObject.FindGameObjectsWithTag("Player");
		}
	}

    public void NewPlayerHasSpawned()
    {
        playersObjects = GameObject.FindGameObjectsWithTag("Player");
    }

	void OnDrawGizmos(){
		//Gizmos.DrawWireSphere(transform.position, 
		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, Quaternion.Euler (0, detectionAngle, 0) * transform.forward * horizontalDetection);
		Gizmos.DrawRay (transform.position, Quaternion.Euler (0, -detectionAngle, 0) * transform.forward * horizontalDetection);
		Gizmos.color = Color.green;
		Gizmos.DrawRay (transform.position, Quaternion.Euler (detectionAngle,0 , 0) * transform.forward * verticalDetection);
		Gizmos.DrawRay (transform.position, Quaternion.Euler (-detectionAngle,0 , 0) * transform.forward * verticalDetection);

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
			//Debug.Log("Player is at the same level as the enemy");
            if (Vector3.Distance(localTransform.position, playerObjectTransform.position) < horizontalDetection)
            {
//				Debug.Log("Player close to the enemy");
                Vector3 playerPositionDelta = playerObjectTransform.position - localTransform.position;
				playerPositionDelta.y=0;
                if (Vector3.Angle(playerPositionDelta, transform.forward) < detectionAngle)
                {
//					Debug.Log("Player is at a good detection angle to the enemy"); 
                    RaycastHit hit;
					Debug.DrawRay(eyes.position, playerPositionDelta);
					Debug.Log(playerPositionDelta);
                    if (Physics.Raycast(eyes.position, playerPositionDelta, out hit, horizontalDetection))
                    {
//						Debug.Log("I've hit the object " + hit.collider.name);
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
