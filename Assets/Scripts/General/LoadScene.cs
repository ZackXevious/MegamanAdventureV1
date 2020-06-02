using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
	public GameObject HowToPlay;

	public void loadScene(string SceneName){
		SceneManager.LoadScene (SceneName);
	}
	public void quitGame(){
		Application.Quit ();
	}
	public void DisplayHowToPlay(){
		HowToPlay.SetActive (true);
		this.gameObject.SetActive (false);
	}
}
