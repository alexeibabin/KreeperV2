using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour {

	public Image fadeOutImage; 
	public Color fadeOutColor;
	public float speedMultiplier = 1f;
	public float errorMargin = 0.01f;
	private bool activate = false;


	// Use this for initialization
	public void Activate(){
		activate = true;
	}
	void Update(){
		if (activate) {
			fadeOutImage.color = Color.Lerp(fadeOutImage.color,fadeOutColor, Time.deltaTime*speedMultiplier);

			if (fadeOutImage.color == fadeOutColor || fadeOutImage.color.a >= fadeOutColor.a-errorMargin){
				Debug.Log("disabling fade out");
				activate = false;
			}
		}
	}
}
