using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public Animator anim;
	public Rigidbody2D rb2d;
	public Collider2D col;
	public Transform myTrans;

	public int direction = 1;
	public float live = 100;

	private float moveSpeed = 10f;
	private float timeIdle = 4f;
	private float move = 0.0f;
	private bool isRunning = false;
	private bool isIdle = false;


	public enum BossActions{
		BossWalk,
		BossRun,
		BossIdle
	}

	public BossActions bossAct = BossActions.BossWalk;

	// Use this for initialization
	void Start () {
		myTrans = GetComponent<Transform>();
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

			case BossActions.BossIdle:
				BossIdle ();
				break;
			}
		}

		if (live <= 30) {
			isRunning = true;

			if (isIdle) 
				bossAct = BossActions.BossIdle;
			else
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

	void BossIdle()
	{
		rb2d.velocity = new Vector2 (0f, 0f);
		anim.Play ("Boss_Idle");
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
}
