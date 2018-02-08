using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
public class Cockroach : MonoBehaviour {
	private Animator anim;
	//private CharacterController2D controller;
	float horizontalSpeed;
	bool isDead = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		//controller = GetComponent<CharacterController2D>();

	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			anim.Play ("die");
			Destroy (this);
		}
		//if (controller.isGrounded) {
			horizontalSpeed = (float)(Mathf.Sin (Time.realtimeSinceStartup) / 30.0);
			transform.Translate (Vector3.left * horizontalSpeed);
			
		/*}
		else  {
			horizontalSpeed = 0;
		}*/
		if (horizontalSpeed < 0f) {
			if (transform.localScale.x > 0f) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Walk");
		}
		else if (horizontalSpeed > 0f) {
			if (transform.localScale.x < 0f) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Walk");
		} else {
			anim.Play ("Idle");
		}

	}
}
