using UnityEngine;
using System.Collections;

public class PlayerSpawnedNotifier : MonoBehaviour {


	private PhotonView photonView;
	// Use this for initialization
	void Start () {
		photonView = GetComponent<PhotonView> ();
		photonView.RPC ("NewPlayerHasSpawned", PhotonTargets.Others);
	}

}
