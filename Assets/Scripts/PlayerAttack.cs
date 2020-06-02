using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public GameObject attackspawn;
	public GameObject forwardAttackObject;
	public GameObject ParticleObject;

	void FixedUpdate(){
		if(Input.GetButtonDown("Attack")){
			Instantiate (forwardAttackObject,attackspawn.transform.position,attackspawn.transform.rotation);
			Instantiate (ParticleObject,attackspawn.transform);
		}
	}
}
