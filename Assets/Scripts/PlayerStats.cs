using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	[SerializeField] int lives;
	[SerializeField] int collectables;
	[SerializeField] int health;
	[SerializeField] int skin;
	[SerializeField] bool[] Mar1 = new bool[5];
	[SerializeField] bool[] Mar2 = new bool[5];
	[SerializeField] bool[] Mar3 = new bool[5];
	[SerializeField] bool[] Cra1 = new bool[5];
	[SerializeField] bool[] Cra2 = new bool[5];
	[SerializeField] bool[] Cra3 = new bool[5];
	[SerializeField] bool[] Pac1 = new bool[5];
	[SerializeField] bool[] Pac2 = new bool[5];
	[SerializeField] bool[] Pac3 = new bool[5];
	[SerializeField] int lifetimeCollectables;

	void Start () {
		if (GameObject.FindGameObjectsWithTag ("PlayerStats").Length > 1) {
			Destroy (this);
		} else {
			DontDestroyOnLoad (this.gameObject);
		}
		Debug.Log ("instantiated player stuffs");

		DontDestroyOnLoad (this);
	}

	//coolMethods------------
	public void GameOver(){
		this.setLives (5);
		this.setCollectables (0);
		this.setHealth (3);
	}

	//Mutators---------------
	public void setLives(int value){
		this.lives = value;
	}
	public void setCollectables(int value){
		this.collectables = value;
	}
	public void setHealth(int value){
		this.health = value;
	}


	//Accessors--------------
	public int getLives(){
		return this.lives;
	}
	public int getCollectables(){
		return this.collectables;
	}
	public int getHealth(){
		return this.health;
	}
}
