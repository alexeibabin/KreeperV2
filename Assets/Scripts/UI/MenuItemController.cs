using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GazeOn))]
[RequireComponent(typeof(LoadNextScene))]
[RequireComponent(typeof(BoxCollider))]
public class MenuItemController : MonoBehaviour {


	private ScaleAnimation scaleAnimation; 
	
	
	// Use this for initialization
	void Start () {
		scaleAnimation = (ScaleAnimation)GetComponentInChildren(typeof(ScaleAnimation));
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
