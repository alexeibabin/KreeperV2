using UnityEngine;
using System.Collections;

public class MenuSightController : MonoBehaviour {

	private CardboardHead playerHead;


	// Use this for initialization
	void Start () {
		playerHead = GetComponentInChildren<CardboardHead>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		
		if (Physics.Raycast(playerHead.Gaze, out hitInfo))
		{
			GazeOn menuButton = hitInfo.collider.gameObject.GetComponent<GazeOn>();

			if(menuButton && menuButton.LookAt())
				menuButton.Use();
		}
	}


}
