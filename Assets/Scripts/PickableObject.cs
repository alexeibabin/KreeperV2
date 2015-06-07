using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	private bool picked = false;

	public void Activate(){
		picked = true;
	}

	void FixedUpdate(){
		if (picked) {
			Destroy(gameObject);
		}
	}
}
