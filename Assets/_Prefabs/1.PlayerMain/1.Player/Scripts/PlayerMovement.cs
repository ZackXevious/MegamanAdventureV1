//Preprossessor Directives------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement Variables---------------------------------------------------------------------------------------
[System.Serializable]
public class CCmovementVariables{

	//Movement variables
	public float maxMoveSpeed=20.0f;
	public float minMoveSpeed=3.0f;
	public float moveAccelTime=50.0f;

	//Turning variables
	public float turnSpeed=20f;

	//Jumping variables
	public float MaxJumpForce = 20f;
	public float MinJumpForce = 3f;
	public float jumpAccelTime = 60f;//length of time it takes to get to the maximum jump velocity
	public float gravity = 10f;

}

//Class Definition-----------------------------------------------------------------------------------------
public class PlayerMovement: MonoBehaviour {
	private GameObject PlayerCamera;
	private GameControllerScript gameController;
	private CharacterController cc;

	public float joyDeadZone;
	public CCmovementVariables moveVars;
	public float CurrMoveZed;
	public bool isJumping;
	public float movey;
	public GameObject jumpSpawn;
	public GameObject DeathSpawn;
	public GameObject SpawnSpawn;

	//Animation related stuffs
	public Animator anim;


//Object Initialization------------------------------------------------------------------------------------
	void Start () {
		isJumping = false;
		//rb = this.GetComponent<Rigidbody>();
		cc = this.GetComponent<CharacterController> ();
		Debug.Log ("Player has spawned successfully");
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameControllerScript> ();
		} if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

		PlayerCamera = GameObject.FindWithTag ("PlayerCamera");

	}
	void Update(){
	}

//Logic Tick Update----------------------------------------------------------------------------------------
	void FixedUpdate(){
		//Move or reorient the player based on current input states
		float currMove = 0;

		if (Mathf.Abs (Input.GetAxis ("Horizontal")) >= this.joyDeadZone || Mathf.Abs (Input.GetAxis ("Vertical")) >= this.joyDeadZone) {

			//Update the player's rotation------------------------------------------------------------------------------------------------------
			Vector3 v = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));
			Quaternion q = Quaternion.FromToRotation (this.transform.forward, v) * PlayerCamera.transform.rotation;
			Quaternion turnTo = this.transform.rotation * q;

			//Apply rotations
			Quaternion currTurn = Quaternion.Slerp (this.transform.rotation, turnTo, Time.deltaTime * moveVars.turnSpeed);
			//Keep Rotations only on the Y axis
			currTurn = Quaternion.Euler (new Vector3 (0.0f, currTurn.eulerAngles.y, 0.0f));

			this.transform.rotation = currTurn;

			var analogInput = Mathf.Abs (Input.GetAxis ("Horizontal")) + Mathf.Abs (Input.GetAxis ("Vertical"));
			currMove = moveVars.maxMoveSpeed * Mathf.Clamp (analogInput, 0, 1);
			if(CurrMoveZed<moveVars.minMoveSpeed){
				CurrMoveZed=moveVars.minMoveSpeed;
			}else if(CurrMoveZed<currMove){
				CurrMoveZed += moveVars.moveAccelTime * Time.deltaTime;
			}else{
				CurrMoveZed=currMove;
			}

		} else {
			CurrMoveZed = 0;
		}

		//Jump Script
		if (Input.GetButton ("Jump") && (cc.isGrounded)) {
			//Play Jump sound
			movey = moveVars.MinJumpForce;
			isJumping = true;
			Instantiate (this.jumpSpawn,this.transform.position,this.transform.rotation);
		}
		if (isJumping && movey < moveVars.MaxJumpForce) {

			if (Input.GetButton ("Jump")) {
				movey += (moveVars.MaxJumpForce / moveVars.jumpAccelTime) * Time.deltaTime;
			} else {
				isJumping = false;
			}

		}else if(movey>=moveVars.MaxJumpForce){
			movey = moveVars.MaxJumpForce;
			isJumping = false;
		}



		if (!cc.isGrounded) {
			movey = movey - moveVars.gravity * Time.deltaTime;
		} else if(!isJumping){
			movey=0;
		}
		if (Mathf.Abs (cc.velocity.y) < 3 && !cc.isGrounded &&!isJumping) {
			movey = movey - moveVars.gravity*2 * Time.deltaTime;
		}
		Vector3 locVel;
		locVel.x = 0.0f;
		locVel.z = CurrMoveZed;
		locVel.y = movey;
		cc.Move (transform.TransformDirection(locVel)*Time.deltaTime);

	}
	void OnDestroy(){
		if (GameObject.FindWithTag ("GameController")!=null) {
			Instantiate (this.DeathSpawn,this.transform.position,PlayerCamera.transform.rotation);
		}

	}
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Hazard")) {
			gameController.HurtPlayer (1);
			Vector3 pointto = new Vector3 (other.transform.position.x,this.transform.position.y,other.transform.position.z);
			this.transform.LookAt (pointto);
			cc.Move (transform.TransformDirection(new Vector3(0.0f,0.0f,-5.0f)));
		}
	}
		
}
/*
 * 
 * docs.unity3d.com/ScriptReference/CharacterController.Move.html
 */
