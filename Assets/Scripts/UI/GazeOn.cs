using UnityEngine;
using System.Collections;

public class GazeOn : MonoBehaviour {

	public Transform target;
	
	private Transform localTransform;
	
	void Start(){
		localTransform = transform;
	}

	void LateUpdate(){
		if (target){
			localTransform.LookAt(target.position);
		}
	}
}
