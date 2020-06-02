using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {
	public GameObject gameController;
	private GameControllerScript GC;
	void Start(){
		//GC=gameController.GetComponent<GameControllerScript>;
	}

	void OnTriggerEnter(Collider other){
		Destroy (other);
		if (other.CompareTag ("Player")||other.CompareTag("Hazard")||other.CompareTag("Crate")) {
			Destroy (other.gameObject);
		}
	}
			
}
