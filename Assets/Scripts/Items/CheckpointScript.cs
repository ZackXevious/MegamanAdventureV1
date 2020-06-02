using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

	// Use this for initialization
	private GameControllerScript gameController;
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameControllerScript> ();
		} if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			
			this.gameController.setRespawnPoint (this.transform);
		}
	}
}
