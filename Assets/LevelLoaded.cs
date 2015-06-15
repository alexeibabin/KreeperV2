using UnityEngine;
using System.Collections;

public class LevelLoaded : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<FadeIn> ().Activate ();
	}

}
