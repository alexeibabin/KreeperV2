using UnityEngine;
using System.Collections;

public class GazeOn : MonoBehaviour {

	public float timeToActivation = 1.5f;

	float currTimer;
	Transform player;

	void Update() {
		if (currTimer > 0)
			currTimer -= Time.deltaTime;

	}

	void Start() {
		player = FindObjectOfType<MenuSightController>().transform;
		transform.LookAt(player);
	}

	public bool LookAt() {
		currTimer += Time.deltaTime * 2;
		Debug.Log("Looking t:" + currTimer);
		
		if (currTimer > timeToActivation)
			return true;
		
		return false;
	}

	public void Use() {
		Debug.Log("Trying to activate!! ");
		GetComponent<LoadNextScene>().Activate();
	}
}
