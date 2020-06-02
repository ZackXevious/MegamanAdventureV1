using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour {

	public Text gemCount;
	public Text PlayerHealth;
	public GameObject healthmeter;
	public Sprite[] healthsprites=new Sprite[4];
	private GameControllerScript gameController;

	// Use this for initialization
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
		gemCount.text = this.gameController.getGems();
		healthmeter.GetComponent<Image> ().sprite=healthsprites[(int)this.gameController.getHealth ()];

		
	}
}
