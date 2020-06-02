using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldController : MonoBehaviour {

	private bool Stage1;
	private bool Stage2;
	private bool Stage3;
	private float MusicVol;
	private float SoundFXVol;
	public float resolutionX;
	public float resolutionY;


	//Player specific
	public int Collectables;
	public bool[] World1=new bool[5];
	public bool[] World2=new bool[5];
	public bool[] World3=new bool[5];
	public int Lives;
	// Use this for initialization
	void Start () {
		saveGame ();
		if (GameObject.FindGameObjectsWithTag ("World").Length > 1) {
			Destroy (this);
		} else {
			DontDestroyOnLoad (this.gameObject);
		}
		Debug.Log ("instantiated the world controller");

		this.loadGame ();
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void saveGame(){
		string location = System.IO.Directory.GetCurrentDirectory ()+"game.txt";
		System.IO.File.WriteAllLines (location,new string[]{"hello","goodbye"});
	}
	public void loadGame(){
		Debug.Log ("Loading game");
	}

	public int GetLives(){
		return this.Lives;
	}
	public int GetCollectables(){
		return this.Collectables;
	}
	public void addLife(){
		this.Lives += 1;
	}
	public void addCollectable(float value){
		this.Collectables += 1;
	}

}
