using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {


	//PlayerStat object, used for keeping track of health, pickups, and savegames
	public GameObject PlayerStatObject;
	public static GameObject playerStats;
	public static PlayerStats playerStatScript;


	//UI elements
	public GameObject pauseScreen;
	public GameObject screenBlackout;

	//Spawnpoint related shenanigans
	public GameObject SpawnPoint;
	public GameObject particleSpawn;

	//Camera related stuffs
	private GameObject CameraRig;
	private CameraRigScript camRigScript;

	//Goal related things
	private GameObject stageGoal;
	private StageGoalScript goalscript;
	private GameObject GoalCamera;

	//Player Related
	public GameObject Player; // Player Prefab
	public Transform CurrentRespawnPoint; //Where you will respawn


	public bool canbehurt;
	public float invinctimer=2;
	public float InvincDuration=2;

	public float respawnTimer=0;
	public float respawnDuration=2;

	public float SkyboxRotate =0;

	// Use this for initialization
	void Awake(){
		if (GameObject.FindGameObjectWithTag ("PlayerStats") == null) {
			Instantiate (this.PlayerStatObject);
		}
		playerStats = GameObject.FindGameObjectWithTag ("PlayerStats");
		playerStatScript = playerStats.GetComponent<PlayerStats> ();
	}
	void Start () {
		
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;

		//Find Camera Rig
		CameraRig = GameObject.FindWithTag ("PlayerCameraRig");
		if (CameraRig != null) {
			camRigScript = CameraRig.GetComponent <CameraRigScript> ();
		} if (camRigScript == null) {
			Debug.Log ("Cannot find the current camera rig!");
		}

		//Find Stage Goal
		stageGoal = GameObject.FindWithTag ("Goal");
		if (stageGoal != null) {
			goalscript = CameraRig.GetComponent <StageGoalScript> ();
		} if (goalscript == null) {
			//Debug.Log ("Cannot find the stage Goal!");
		}
		//stageGoal.SetActive (false);



		this.setRespawnPoint (this.transform);
	}

	// Update is called once per frame
	void Update () {
		RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation")+SkyboxRotate*Time.time);
		if (Input.GetButtonDown ("Pause")) {
			
			this.Pause ();
		}

		//Turn off invincibility


		//Check to see if there are any more pickup items
		/*if (GameObject.FindGameObjectsWithTag ("Pickup").Length <= 0) {
			activateGoal ();
		}*/
		
	}

	void FixedUpdate(){
		if (GameObject.FindWithTag ("Player") == null) {
			if(respawnTimer==0){
				this.respawnTimer = this.respawnDuration;
			}
			if (respawnTimer <= 0) {
				respawnPlayer ();
			} else {
				respawnTimer -= Time.deltaTime;
			}

		} else {
			respawnTimer = 0;

		}
		if (this.getHealth()<=0) {
			//Debug.Log ("The Player is out of health!");
			Destroy (GameObject.FindWithTag ("Player"));

		}

		if (!canbehurt) {
			if (invinctimer > 0) {
				invinctimer -= Time.deltaTime;
			} else {
				canbehurt = true;
				invinctimer = InvincDuration;
			}
		}
	}
	public void Pause(){
		if (pauseScreen.activeInHierarchy == false) {
			pauseScreen.SetActive (true);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
		} else {
			pauseScreen.SetActive (false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;
		}
			
		
	}

	//GemRelated
	public void addGems(int value){
		playerStatScript.setCollectables (playerStatScript.getCollectables () + value);


	}
	public string getGems(){
		if (playerStatScript.getCollectables () >= 25 && this.getHealth () < 3) {
			this.HealPlayer (1);
			playerStatScript.setCollectables (playerStatScript.getCollectables () % 25);
		} else if(playerStatScript.getCollectables () >= 25){
			playerStatScript.setCollectables (25);
		}
		return "Gems: " + playerStatScript.getCollectables ();
	}
	public void HurtPlayer(float value){
		if (canbehurt) {
			playerStatScript.setHealth ((int)(playerStatScript.getHealth () - value));
			//Debug.Log ("player is hurt");
			canbehurt = false;
		}
	}
	public void HealPlayer(float value){
		if(this.getHealth()<3)
		playerStatScript.setHealth((int)(playerStatScript.getHealth()+value)) ;
	}
	//Health Related
	public float getHealth(){
		return playerStatScript.getHealth ();
	}

	//SceneRelated:
	public void MainMenu(){
		SceneManager.LoadScene ("StartMenu");
	}
	public void QuitGame(){
		Application.Quit ();
	}
	public void respawnPlayer(){
		Instantiate (Player,CurrentRespawnPoint.position,CurrentRespawnPoint.rotation);
		this.CameraRig.transform.position = CurrentRespawnPoint.transform.position;
		this.CameraRig.transform.rotation = CurrentRespawnPoint.transform.rotation;

		playerStatScript.setHealth (3);
		invinctimer = InvincDuration;
		canbehurt = false;
	}
	public void setRespawnPoint(Transform other){
		if (CurrentRespawnPoint != other) {
			this.CurrentRespawnPoint = other;
			Vector3 SpawnPosition = new Vector3 (other.transform.position.x,other.transform.position.y+2,other.transform.position.z);

			Instantiate (particleSpawn,SpawnPosition,particleSpawn.transform.rotation);
		}
		//Debug.Log ("Current respawn point set to "+other);
	}


	public void activateGoal(){
		stageGoal.SetActive (true);
	}
}
