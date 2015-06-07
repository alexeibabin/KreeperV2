using UnityEngine;
using System.Collections;

public class PickableMark : MonoBehaviour {

	private Material mat;
	private float currentShinyness = 0; 
	private bool scaleUpShinyness = true;
	void Start(){
		//gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_Shininess",0.1f);
		mat = gameObject.GetComponent<MeshRenderer> ().material;
	}

	void FixedUpdate(){
		if (scaleUpShinyness) {
			if (currentShinyness<0.97){
				currentShinyness = Mathf.Lerp(currentShinyness,1,Time.fixedDeltaTime);
			}else{
				scaleUpShinyness = false;
			}
		} else {
			if (currentShinyness>0.03){
				currentShinyness = Mathf.Lerp(currentShinyness,0,Time.fixedDeltaTime);
			}else{
				scaleUpShinyness = true;
			}
		}
		mat.SetFloat ("_Shininess", currentShinyness);
	}

}
