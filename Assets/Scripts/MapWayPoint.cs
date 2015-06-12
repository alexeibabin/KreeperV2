using UnityEngine;
using System.Collections;
using System;

public class MapWayPoint : MonoBehaviour {

    public float initialHeight = 1f;
    

    public float timeToActivation = 0.5f;
    public float drawDistance = 25f;

    private float currTimer;
    private ParticleSystem particles;
    private Color defaultColor;


	// Use this for initialization
	void Start () {
        particles = GetComponentInChildren<ParticleSystem>();
        defaultColor = particles.startColor;

        PositionInit();
	}

    void Awake()
    {
        PlayerSightController.PlayerMovedEvent += DistanceDraw;
    }

    private void DistanceDraw(Vector3 obj)
    {
        if (Vector3.Distance(transform.position, obj) < drawDistance)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        //Debug.Log("Dist - " + Vector3.Distance(transform.position, obj));
    }

    private void PositionInit()
    {
        RaycastHit hit;
        if (!Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
            return;

        transform.position = hit.point + new Vector3(0, initialHeight, 0);
		particles.transform.position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (currTimer > 0)
            currTimer -= Time.deltaTime;
        else
            particles.startColor = defaultColor;
	}

    //  Returns true when it's time to activate, false otherwise.
    public bool LookAt(Transform player)
    {
        currTimer += Time.deltaTime * 2;

        if (particles)
            particles.startColor = Color.red;

        if (currTimer > timeToActivation)
            return true;

        return false;
    }
}
