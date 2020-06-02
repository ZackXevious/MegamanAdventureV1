using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
	public int Health=1;
	public GameObject hurtSpawn;
	public string adverseTo;
	public bool isFloating = false;
	public float numChunk=1;
	public GameObject chunk;
	public float numItems=1;
	public GameObject itemspawn;
	public float numDeath=1;
	public GameObject deathspawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Health <= 0) {
			for(int x=0;x<numDeath;x++){
				Instantiate (deathspawn,this.transform.position,this.transform.rotation);
			}
			for(int x=0;x<numChunk;x++){
				Instantiate (chunk,this.transform.position,this.transform.rotation);
			}
			for(int x=0;x<numItems;x++){
				Instantiate (itemspawn,this.transform.position,this.transform.rotation);
			}
			Destroy (this.gameObject);
		}
		
	}
	void OnTriggerEnter(Collider other){
		//Debug.Log ("This is happening");
		if (other.CompareTag (adverseTo)) {
			
			Health -= 1;
			if(Health>0){
				Instantiate (hurtSpawn,this.transform.position,this.transform.rotation);
			}
			Destroy (other.gameObject);
		}
	}
}
