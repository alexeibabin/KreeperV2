using UnityEngine;
using System.Collections;

public abstract class ControllableObject : MonoBehaviour {

	public abstract void Activate ();
	
	public abstract void Deactivate();
}
