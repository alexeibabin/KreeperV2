using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

	public enum Scenes {
		MainMenu,
		FirstLevel,
		MultiplayerLevel
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
		}
	}
}
