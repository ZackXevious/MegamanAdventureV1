using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

	public int AddValue=1;
	public float minDistance=10;

	public GameObject deathspawn;
	// Use this for initialization
	private GameControllerScript gameController;
	private PlayerStats playerstat;
	private GameObject Player;
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameControllerScript> ();
		} if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		Player = GameObject.FindWithTag ("Player");
		if (Player != null) {
			float tempDist = Vector3.Distance (this.transform.position, Player.transform.position);
			if ( tempDist < minDistance) {
				this.transform.position = Vector3.Lerp (this.transform.position,Player.transform.position,Time.deltaTime*(minDistance*2)/tempDist);
			}
			GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
			if (gameControllerObject != null) {
				gameController = gameControllerObject.GetComponent <GameControllerScript> ();
			} if (gameController == null) {
				Debug.Log ("Cannot find 'GameController' script");
			}
		}

		
	}
	void OnTriggerStay(Collider other){
		//Debug.Log ("This is happening");
		if (other.CompareTag ("Player")) {
			Instantiate (deathspawn,this.transform.position,this.transform.rotation);
			gameController.addGems (this.AddValue);
			Destroy (this.gameObject);
		}
	}
}
