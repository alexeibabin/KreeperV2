using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	public float timeToActivation = 0.5f;
	public GameObject dissapearedEffect;
	private ParticleSystem particles;
	private Color defaultColor;

	private float currTimer;

	private bool picked = false;

	void Start(){
		particles = GetComponentInChildren<ParticleSystem>();
		defaultColor = particles.startColor;

	}

	public void PickUp(){
		picked = true;
	}

	void Update(){
		if (currTimer > 0)
			currTimer -= Time.deltaTime;
		else
			particles.startColor = defaultColor;
	}

	public bool LookAt(Transform player)
	{
		currTimer += Time.deltaTime * 2;
		
		if (particles) 
			particles.startColor = Color.white;

		
		if (currTimer > timeToActivation)
			return true;
		
		return false;
	}

	void FixedUpdate(){
		if (picked) {
			if (dissapearedEffect){
				Instantiate(dissapearedEffect,transform.position,dissapearedEffect.transform.rotation);
			}
			Destroy(gameObject);
		}
	}
}
