using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class WayPointController : MonoBehaviour
{
    public enum WaypointRotate
    {
        GlobalRight,
        GlobalLeft,
        WaypointForward,
        PlayerForward
    }
    public Canvas canv;


    public WaypointRotate playerRotation;
    //public bool reversePlayerRotation = false;

    private Vector3 staticRotate;
    private Vector3 staticPosition;
    private Transform playerTransform;

    void Start()
    {
        canv.gameObject.SetActive(false);
        staticPosition = transform.position;
        staticRotate = transform.forward;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public Vector3 getPosition()
    {
        return staticPosition;
    }

    public Vector3 getRotation()
    {
        return staticRotate;
    }

    void Update()
    {
        gameObject.transform.LookAt(playerTransform.position);
    }

    public void Activate()
    {
        canv.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        canv.gameObject.SetActive(false);
    }

}
