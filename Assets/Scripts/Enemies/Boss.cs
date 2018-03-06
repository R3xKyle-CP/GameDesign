using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public Animator anim;
	public Rigidbody2D rb2d;

	public int direction = -1;

	private float moveSpeed = 10f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		BossWalking ();
	}

	void BossWalking()
	{
		switch(direction)
		{
			//moving left
			case -1:
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, -transform.localScale.z);
				rb2d.velocity = new Vector2 (-moveSpeed, rb2d.velocity.y);
				anim.Play ("Boss_Walking");
				break;

			case 1:
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
				rb2d.velocity = new Vector2 (moveSpeed, rb2d.velocity.y);
				anim.Play ("Boss_Walking");
				break;

			break;
		}
	}

	void BossRunning()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Boss_Max_Boundary") {
			direction = direction * -1;
		}
	}
}
