using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOverTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Destroy (this.gameObject,5);
	}
	void OnDestroy(){
		SceneManager.LoadScene ("6Finale");
	}
}
