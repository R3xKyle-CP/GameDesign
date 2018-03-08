using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public Animator anim;
	public Rigidbody2D rb2d;
	public Collider2D col;

	public int direction = 1;
	public float live = 100;

	private float moveSpeed = 10f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		col = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (live <= 0) {
			rb2d.velocity = new Vector2 (0f, 0f);
			anim.Play ("Boss_Dying");
			col.enabled = false;
		} else {
			BossWalking ();
		}
	}

	void BossWalking()
	{
		switch(direction)
		{
			//moving left
			case -1:
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				rb2d.velocity = new Vector2 (-moveSpeed, rb2d.velocity.y);
				//anim.Play ("Boss_Walk");
				break;

			case 1:
				//transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
				rb2d.velocity = new Vector2 (moveSpeed, rb2d.velocity.y);
				//anim.Play ("Boss_Walk");
				break;

			break;
		}
	}

	void BossRunning()
	{

	}

	public void decreaseLive()
	{
		Debug.Log ("Decreased Boss Life");
		this.live -= 10;
		return;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Boss_Max_Boundary") {
			direction = direction * -1;
		}
	}
}
