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
		//this.gameObject.SetActive (false);
		if (collision.gameObject.CompareTag ("Bat") ) 
		{
			
			collision.gameObject.GetComponent<Bat> ().decreaseLive ();
			//this.gameObject.SetActive (false);
		}
		if (collision.gameObject.CompareTag ("Cockroach") ) 
		{
			
			collision.gameObject.GetComponent<Cockroach> ().decreaseLive ();
			//Destroy (this);
			//this.gameObject.SetActive (false);
		}
		if (!collision.gameObject.CompareTag ("Player") ){
			this.gameObject.SetActive (false);
		}
	}
}
