using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour {

	public enum Scenes {
		MainMenu,
		FirstLevel,
		MultiplayerLevel,
		Quit
	}

	public FadeIn fadeInSequence;
	public FadeOut fadeOutSequence;

	public Scenes loadLevel;

	public void Activate(){
		switch (loadLevel){
			case Scenes.FirstLevel:
				StartCoroutine("LoadLevel","FirstLevel");
				break;
			case Scenes.MainMenu:
			StartCoroutine("LoadLevel","MainMenu");
				break;
			case Scenes.MultiplayerLevel:
			StartCoroutine("LoadLevel","Mutli Scene");
				break;
			case Scenes.Quit:
				Application.Quit();
				break;
		}
	}

	IEnumerator LoadLevel(string levelName){
		fadeOutSequence.Activate ();
		yield return new WaitForSeconds(1);
		Application.LoadLevel(levelName);
	}
}
