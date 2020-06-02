using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageGoalScript : MonoBehaviour {
	public string NextScene;
	private GameControllerScript gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameControllerScript> ();
		} if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		Debug.Log ("Stage Goal loaded successfully");
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			SceneManager.LoadScene (this.NextScene);
		}
	}
}
