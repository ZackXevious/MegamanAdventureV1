using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public GameObject[] nodes;
	public int[] order;

	public float ViewDistance;
	public GameObject currTarget;
	public int currnode=0;

	public float maxDistanceFromNode=20.0f;
	public float minDistanceFromNode=5.0f;

	public float moveSpeed = 10.0f;
	public float waittime = 5.0f;
	public bool active = true;
	private float currtimer;
	public float Damage;

	private GameObject player;
	private GameControllerScript gameController;

	// Use this for initialization
	void Start () {
		Debug.Log (order.Length);
		this.currnode = this.order [0];
		this.currTarget = nodes [currnode];




		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameControllerScript> ();
		} if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		currtimer = waittime;		
	}

	void FixedUpdate(){
		player = GameObject.FindGameObjectWithTag ("Player");
		if (currTarget == null) {
			this.advanceNode ();
		}

		//Active, currently hunting for player
		if (active) {
			if (this.checkDistanceToNode () < this.minDistanceFromNode) {
				this.advanceNode ();
			}
			if (currTarget != nodes [order [currnode]]) {
				if (this.checkDistanceToNode () > this.maxDistanceFromNode) {
					currTarget = this.nodes [order [currnode]];
				}
			}

			Vector3 pointTowards = new Vector3 (this.currTarget.transform.position.x,this.transform.position.y,this.currTarget.transform.position.z);
			transform.LookAt (pointTowards);
			transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);

			if (Vector3.Distance (player.transform.position, this.transform.position) < 10) {
				currTarget = player;
			}

			//Not active, counter is done
		} else if (currtimer <= 0) {
		active = true;
		currtimer = waittime;

		//If not active, and the timer is not done
		} else {
			currtimer -= Time.deltaTime;
		}


		if (player != null){
			}else {
				player=GameObject.FindWithTag ("Player");
			}
	}


//Handling collisions/triggers-----------------------------------------------------------------------
	void onCollisionEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			
		} else if(other.gameObject.CompareTag("Enemy")){
			float otherDist = other.gameObject.GetComponent<EnemyAI> ().checkDistanceToNode ();
			if(otherDist<=this.checkDistanceToNode()){
				this.reverseNode ();
			}
		}
	}

	void OnTriggerStay(Collider other){
		if(other.CompareTag("Player") && active){

			gameController.HurtPlayer (1*Damage);
			active = false;
			this.currTarget = nodes [order [currnode]];
		}
	}



//Working with the nodes-------------------------------------------------------------------------
	public float checkDistanceToNode(){
		return Vector3.Distance (this.transform.position,nodes[order[currnode]].transform.position);
	}
	void reverseNode(){
		currnode -= 1;
		if(currnode<0){
			Debug.Log ("GOING BACK TO THE FUTURE");
			currnode = (order.Length - 1);
		}
		Debug.Log (currnode);
		this.currTarget = nodes [order[currnode]];
	}
	void advanceNode(){
		currnode += 1;
		if(currnode>order.Length - 1){
			Debug.Log ("GOING BACK TO THE PAST");
			currnode = 0;
		}
		Debug.Log (order[currnode]);
		this.currTarget = nodes [order[currnode]];
	}
}
