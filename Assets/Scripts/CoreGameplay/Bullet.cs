using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		///<MichaelAnnie>
		//If the bullet collides with an enemy, then deal damage to the enemy
		///</MichaelAnnie>
		if (collision.gameObject.CompareTag ("Bat") ) 
		{
			
			collision.gameObject.GetComponent<Bat> ().decreaseLive ();
		}
		if (collision.gameObject.CompareTag ("Cockroach") ) 
		{
			
			collision.gameObject.GetComponent<Cockroach> ().decreaseLive ();
		}
		///<MichaelAnnie>
		//Otherwise the bullet is set as inactive
		///</MichaelAnnie>
		if (!collision.gameObject.CompareTag ("Player") ){
			this.gameObject.SetActive (false);
		}
	}
}
