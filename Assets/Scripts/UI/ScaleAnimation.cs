using UnityEngine;
using System.Collections;

public class ScaleAnimation : MonoBehaviour {

	public Vector3 startScale = new Vector3(0.1f,0.1f,0.1f); 
	public Vector3 targetScale = new Vector3(1f,1f,1f); 
	
	[RangeAttribute(0,0.1f)]
	public float animationPersition=0.1f;
	
	public bool scaleUp = false;
	
	private bool hasFinished= true;
	
	private Transform localTransform;
	

	// Use this for initialization
	void Start () {
		localTransform = transform;
		localTransform.localScale = startScale; 
	}
	
	// Update is called once per frame
	void Update () {
		if (scaleUp){
			if (! localTransform.localScale.AlmostEquals(targetScale, 0.1f)){
                hasFinished = false;
                localTransform.localScale = Vector3.Slerp(localTransform.localScale, targetScale, Time.deltaTime);
            }
            else
            {
                hasFinished = true;
            }
        }
        else
        {
			if (! localTransform.localScale.AlmostEquals(startScale,0.1f)){
				hasFinished = false;
				localTransform.localScale = Vector3.Slerp(localTransform.localScale,startScale, Time.deltaTime);
			}else{
				hasFinished = true;
			}
		}
	}
	
	public void Animate(){
		if (hasFinished){
			scaleUp = !scaleUp;
		}
	}
	
	
	
	
}
