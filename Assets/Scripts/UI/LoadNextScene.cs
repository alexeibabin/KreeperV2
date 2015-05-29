using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour {

	public enum Scenes {
		MainMenu,
		FirstLevel,
		MultiplayerLevel,
		Quit
	}
	
	public Scenes loadLevel;

	public void Activate(){
		switch (loadLevel){
			case Scenes.FirstLevel:
				Application.LoadLevel("FirstLevel");
				break;
			case Scenes.MainMenu:
				Application.LoadLevel("MainMenu");
				break;
			case Scenes.MultiplayerLevel:
				Application.LoadLevel("MultiplayerLevel");
				break;
			case Scenes.Quit:
				Application.Quit();
				break;
		}
	}
}
