using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
public class Cockroach : MonoBehaviour {
	private Animator anim;
	//private CharacterController2D controller;
	float horizontalSpeed = 3f ;
	public bool moveright;
	bool isDead = false;
	public Transform wallCheck;
	public float wallCheckRadius;
	private bool hittingWall;
	public LayerMask whatIsWall;
	private bool atEdge;
	public Transform edgeCheck;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		//controller = GetComponent<CharacterController2D>();

	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			anim.Play ("die");
			//Destroy (this);
		}
		//if (controller.isGrounded) {
			//horizontalSpeed = (float)(Mathf.Sin (Time.realtimeSinceStartup) / 30.0);

			
		/*}
		else  {
			horizontalSpeed = 0;
		}*/
		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		atEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);
		if (hittingWall || !atEdge)
			moveright = !moveright;
		if (moveright) {
			//transform.Translate (Vector3.right);
			GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalSpeed, GetComponent<Rigidbody2D>().velocity.y);
			if (transform.localScale.x > 0f) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Walk");
		} else {
			//transform.Translate (Vector3.left);
			GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, GetComponent<Rigidbody2D>().velocity.y);
			if (transform.localScale.x < 0f ) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Walk");
		}
		/*if (horizontalSpeed < 0f) {
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
		}
		if (isDead){
			anim.Play ("Idle");
		}?*/

	}
}
