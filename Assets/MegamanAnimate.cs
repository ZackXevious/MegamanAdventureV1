using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanAnimate : MonoBehaviour {
	bool firing=false;
	public Animator anim;
	private bool isInAir;
	public GameObject playercube;
	public GameObject chest;
	public GameObject waist;
	public GameObject head;
	public GameObject target;
	public GameObject Arm1;
	public GameObject Arm2;
	public GameObject camrig;
	public CharacterController PlayerCC;


	public float delay=2;
	public float tempdelay=0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		PlayerCC = playercube.GetComponent<CharacterController>();
		isInAir = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		if (firing) {
			if (!Physics.Raycast (playercube.transform.position, transform.TransformDirection (Vector3.down), 1.1f) && !isInAir) {
				//Debug.Log ("jumpupgun");
				anim.Play ("MM_JumpUP_Gun");
				isInAir = true;
			} else if (isInAir == true ) {
				if (Physics.Raycast (playercube.transform.position, transform.TransformDirection (Vector3.down), 1.1f)) {
					anim.Play ("MM_JumpDown_Gun");
					//Debug.Log ("Fallinggun");
					isInAir = false;
				} else {
					//anim.Play ("MM_JumpAir_Gun");
				}
			}else if (Mathf.Abs (Input.GetAxis ("Horizontal")) + Mathf.Abs (Input.GetAxis ("Vertical"))>.1) {
				anim.speed = Mathf.Clamp( Mathf.Abs (Input.GetAxis ("Horizontal")) + Mathf.Abs (Input.GetAxis ("Vertical")),0,1);
				anim.SetBool ("ismoving",true);
				anim.Play ("MM_Run_Gun");
			} else{
				anim.speed = 1;
				anim.Play ("MM_Idle_Gun");
				anim.SetBool ("ismoving",false);
			}
		} else {
			if (!Physics.Raycast (playercube.transform.position, transform.TransformDirection (Vector3.down), 1.1f) && !isInAir) {
				//Debug.Log ("jumpup");
				anim.Play ("MM_JumpUP_Default");
				isInAir = true;
			} else if (isInAir == true ) {
				if (Physics.Raycast (playercube.transform.position, transform.TransformDirection (Vector3.down), 1.1f)) {
					anim.Play ("MM_JumpDown_Default");
					//Debug.Log ("Falling");
					isInAir = false;
				} else {
					//anim.Play ("MM_JumpAir_Default");
				}
			}else if (Mathf.Abs (Input.GetAxis ("Horizontal")) + Mathf.Abs (Input.GetAxis ("Vertical"))>.1) {
				anim.speed = Mathf.Clamp( Mathf.Abs (Input.GetAxis ("Horizontal")) + Mathf.Abs (Input.GetAxis ("Vertical")),0,1);
				anim.SetBool ("ismoving",true);
				anim.Play ("MM_Run_Default");
			} else{
				anim.speed = 1;
				anim.Play ("MM_Idle_Default");
				anim.SetBool ("ismoving",false);
			}
		}

		if(Input.GetButton("Attack")){
			tempdelay = delay;
			firing = true;
			anim.SetBool ("firing",true);

		}
		if (firing) {
			if (tempdelay > 0) {
				tempdelay -= Time.deltaTime;
			} else {
				tempdelay = 0;
				firing = false;
				anim.SetBool ("firing",false);
			}
		}
	}
	void LateUpdate(){
		

		//Arm1.transform.rotation = Quaternion.Slerp (chest.transform.rotation, , 1.0f);
		//Arm2.transform.rotation = Quaternion.Slerp (chest.transform.rotation, camrig.transform.rotation, 1.0f);
	}
}
