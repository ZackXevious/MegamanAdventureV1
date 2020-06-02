using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerDumb : MonoBehaviour {
	public GameObject player;
	public GameObject Shotspawn1;
	public GameObject Shotspawn2;
	public GameObject bullet;
	public GameObject bulletEffect;
	Rigidbody rb;
	private GameControllerScript gameController;

	//Movement Speed
	public float moveSpeed = 1.0f;
	public float turnSpeed = 1.0f;
	public float distance = 10.0f;
	public float waittime = 2.0f;
	public float ShotDelay=3.0f;
	bool active = true;
	public bool huntplayer = true;
	private float currtimer;
	private float shotTimer;


	// Use this for initialization
	void Start () {
		shotTimer = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		rb = this.GetComponent<Rigidbody> ();

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
				gameController = gameControllerObject.GetComponent <GameControllerScript> ();
			} if (gameController == null) {
				Debug.Log ("Cannot find 'GameController' script");
			}
		currtimer = waittime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player != null) {
			//If currently active as a robot, and trying to hunt down 
			if (active && huntplayer) {
				Vector3 Lookat = new Vector3 (player.transform.position.x,this.transform.position.y,player.transform.position.z);
				Quaternion desiredTurn=Quaternion.LookRotation (Lookat-this.transform.position);
				this.transform.rotation = Quaternion.Lerp (this.transform.rotation,desiredTurn,Time.deltaTime*turnSpeed);
				rb.velocity = transform.TransformDirection (new Vector3 (0.0f, rb.velocity.y, moveSpeed));

				if(this.Shotspawn1!=null && this.Shotspawn2!=null && this.bullet!=null){
					if(shotTimer<=0){
						Instantiate (this.bullet,Shotspawn1.transform.position,Shotspawn1.transform.rotation);
						Instantiate (this.bullet,Shotspawn2.transform.position,Shotspawn2.transform.rotation);
						Instantiate (this.bulletEffect,Shotspawn1.transform);
						Instantiate (this.bulletEffect,Shotspawn2.transform);
						shotTimer=ShotDelay;
					}else{
						shotTimer -= Time.deltaTime;
					}

				}

				

		}else if (active && !huntplayer) {
				//run in circles
				/*this.transform.LookAt ();
				//actually move
				rb.velocity = transform.TransformDirection (new Vector3 (0.0f, rb.velocity.y, moveSpeed));*/
				if (Vector3.Distance (player.transform.position, this.transform.position) < this.distance) {
					this.huntplayer = true;
				}

				//If not active, and the timer is done
			} else if (currtimer <= 0) {
				active = true;
				currtimer = waittime;

				//If not active, and the timer is not done
			} else {
				currtimer -= Time.deltaTime;

			}

		}else {
			this.huntplayer = false;
			player=GameObject.FindWithTag ("Player");
		}
	}

			
	void OnTriggerStay(Collider other){
		if(other.CompareTag("Player") && active){

			gameController.HurtPlayer (1);
			active = false;
			huntplayer = false;
		}
	}
	void OnTriggerEnter(Collider other){
		//Debug.Log ("This is happening");
		if (other.CompareTag ("Attack")) {
			this.huntplayer = true;
		}
	}
}
