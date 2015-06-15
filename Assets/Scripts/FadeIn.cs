using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

	public Image fadeInImage; 
	public Color fadeInColor;
	public float speedMultiplier = 1f;
	public float errorMargin = 0.01f;

	private bool activate = false;
	
	// Use this for initialization
	public void Activate(){
		activate = true;
	}
	void Update(){
		if (activate) {
			fadeInImage.color = Color.Lerp(fadeInImage.color,fadeInColor, Time.deltaTime*speedMultiplier);

			if (fadeInImage.color == fadeInColor || fadeInImage.color.a <= fadeInColor.a+errorMargin){
				Debug.Log("disabling fade in");
				activate = false;
			}
		}
	}
}
