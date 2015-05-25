using UnityEngine;
using System.Collections;

public class LoadLevelBehaviour : BaseBehaviour {
	public int levelNumber=1;
	public override void Action(GameObject target){
		Application.LoadLevel(levelNumber);
		
	}

}
