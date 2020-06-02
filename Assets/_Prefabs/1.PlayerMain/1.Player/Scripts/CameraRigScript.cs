using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigScript : MonoBehaviour {
	
	public float xSpeed;
	public float ySpeed;
	public float upLimit;
	public float downLimit;


	public float delay;
	private float tempdelay;

	public float minDistPlayer=0.1f;
	public float moveSnapTo=15;
	public float rotSnapTo=5;
	public bool lockStrafe;
	public bool followPlayer;

	private GameObject Player;
	public GameObject PlayerCamera;

	// Use this for initialization
	void Start () {
		followPlayer = lockStrafe;
		if (PlayerCamera == null) {
			PlayerCamera = GameObject.FindGameObjectWithTag ("PlayerCamera");
			if(PlayerCamera==null){
				Debug.Log("Camera NOT FOUND");
			}
		}
		SearchForPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		if (Player != null) {
			//Camera Horizontal Movement
			Vector3 PlayerLocation = Player.transform.position;
			Vector3 desiredLoc = new Vector3 (PlayerLocation.x, PlayerLocation.y + 2, PlayerLocation.z);


			this.transform.position = Vector3.Lerp (this.transform.position,desiredLoc,Time.deltaTime*moveSnapTo);
			//Quaternion playerRotate;
			//Quaternion.Lerp ();
			Vector3 PlayerXY = new Vector3 (Player.transform.position.x,0.0f,Player.transform.position.z);
			Vector3 CamRigXY = new Vector3 (this.transform.position.x,0.0f,this.transform.position.z);
			float DistToPlayer = Vector3.Distance (PlayerXY,CamRigXY);
			this.transform.Rotate (0.0f, Input.GetAxis ("Cam X") * this.xSpeed, 0.0f);

			//Camera Vertical Movement
			float CamVertMovement = Input.GetAxis ("Cam Y");
			float CamRelativeToRig = this.PlayerCamera.transform.localRotation.x*Mathf.Rad2Deg;
			//Debug.Log (CamRelativeToRig);


		

			if ((CamVertMovement < 0 && (CamRelativeToRig + CamVertMovement) < this.upLimit)
			    || (CamVertMovement > 0 && (CamRelativeToRig - CamVertMovement) > this.downLimit)) {

				this.PlayerCamera.transform.RotateAround (
					this.transform.position, this.transform.TransformDirection (Vector3.left),
					Input.GetAxis ("Cam Y")*ySpeed);
			}

		} else {
			SearchForPlayer ();
		}
		
	}
	public void SearchForPlayer(){
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

}
