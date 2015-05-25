using UnityEngine;
using System.Collections;

public class ControllableDoor : ControllableObject {

	private Animator animator ; 
	
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void Activate()
	{
		animator.SetTrigger("Open");
	}
	
	public override void Deactivate ()
	{
		animator.SetTrigger("Close");
	}
}
