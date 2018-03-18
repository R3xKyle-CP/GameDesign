using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Animator anim;
	void Start () {
		anim = GetComponent<Animator> ();
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		///<MichaelAnnie>
		//If the bullet collides with an enemy, then deal damage to the enemy
		///</MichaelAnnie>
		this.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
		if (collision.gameObject.CompareTag ("Bat") ) 
		{
			
			collision.gameObject.GetComponent<Bat> ().decreaseLive ();
		}
		if (collision.gameObject.CompareTag ("Cockroach") ) 
		{
			
			collision.gameObject.GetComponent<Cockroach> ().decreaseLive ();
		}
		if (collision.gameObject.CompareTag ("Boss")) {
			
			collision.gameObject.GetComponent<Boss> ().decreaseLive ();
		}
		///<MichaelAnnie>
		//Otherwise the bullet is set as inactive
		///</MichaelAnnie>
		if (!collision.gameObject.CompareTag ("Player") ){
			anim.Play ("Explode1");
			Destroy(this.gameObject,1);
		}
	}
}
