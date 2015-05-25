using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class ButtonSelector : MonoBehaviour
{
    public EventSystem eventSystem;
    public float rayCastDistance = 10f;

    private GameObject player;
    private BaseBehaviour buttonBehaviour;
    private CardboardHead head;
    private bool startedCoroute = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        head = Camera.main.GetComponent<StereoController>().Head;
        buttonBehaviour = GetComponent<BaseBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var pointer = new PointerEventData(EventSystem.current);

        bool isLookedAt = GetComponent<BoxCollider>().Raycast(head.Gaze, out hit, rayCastDistance);
        PointerEventData eventData = new PointerEventData(eventSystem);
        if (isLookedAt && !startedCoroute)
        {
            Debug.Log("Sending Click event to button");

            GetComponent<Button>().OnPointerDown(eventData);
            StartCoroutine("TimeoutCoroutine");
            startedCoroute = true;
        }
        else if (!isLookedAt)
        {
            StopCoroutine("TimeoutCoroutine");
            startedCoroute = false;
        }
    }

    public IEnumerator TimeoutCoroutine()
    {
        yield return new WaitForSeconds(1f);
        startedCoroute = false;
        Debug.Log("Finished Coroutine");
        buttonBehaviour.Action(player);
    }


}
