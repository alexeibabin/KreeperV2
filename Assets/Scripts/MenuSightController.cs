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
	
	}
}
