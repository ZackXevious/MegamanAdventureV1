using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHazard : MonoBehaviour {
	public GameControllerScript gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControllerScript> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			
		}
	}
}
