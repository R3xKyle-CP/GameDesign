using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <Donovan>
/// Script that controls the boss
/// </Donovan>
public class Boss : MonoBehaviour {

	public Animator anim;
	public Rigidbody2D rb2d;
	public Collider2D col;
	public Transform myTrans;

	public int direction = 1;
	public float live = 100;

	public float moveSpeed = 10f;

	AudioSource fxSound;
	public AudioClip hitSound;


	public enum BossActions{
		BossWalk,
		BossRun,

	}

	public BossActions bossAct = BossActions.BossWalk;

	// Use this for initialization
	void Start () {
		myTrans = GetComponent<Transform>();
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		col = GetComponent<Collider2D> ();
		fxSound = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (live <= 0) {
			rb2d.velocity = new Vector2 (0f, 0f);
			anim.Play ("Boss_Dying");
			col.enabled = false;
			Destroy (this.gameObject, 1.3f);
		} 
		else 
		{
			switch(bossAct)
			{
			case BossActions.BossWalk:
				BossWalking ();
				break;

			case BossActions.BossRun:
				BossRunning ();
				break;
			}
		}

		if (live <= 40) {
			bossAct = BossActions.BossRun;
		}
	}

	void BossWalking()
	{
		switch(direction)
		{
		case 1:
			rb2d.velocity = new Vector2 (-moveSpeed, rb2d.velocity.y);
			break;

		case -1:
			rb2d.velocity = new Vector2 (moveSpeed, rb2d.velocity.y);
			break;
		}
	}

	void BossRunning()
	{
		switch(direction)
		{
		case 1:
			rb2d.velocity = new Vector2 (-moveSpeed * 2, rb2d.velocity.y);
			anim.Play ("Boss_Running");
			break;

		case -1:
			rb2d.velocity = new Vector2 (moveSpeed * 2, rb2d.velocity.y);
			anim.Play ("Boss_Running");
			break;

			break;
		}
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
			Vector3 theScale = myTrans.localScale;

			theScale.x *= -1;

			myTrans.localScale = theScale;

			direction = direction * -1;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
			fxSound.Play ();
	}
}
