using UnityEngine;
using System.Collections;

public class PickablesSerializer : Photon.MonoBehaviour {
	private bool picked = false;
	
	public void Activate(){
		picked = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (picked) {
			PhotonNetwork.Destroy (gameObject);
		}
	
	}
}
