using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Donovan>
/// Code based on this turorial: https://www.youtube.com/watch?v=m3alCI5yTHY
/// </Donovan>
public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float fallDelay;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}
		
	//checks for player or ground
	//Player: start falling
	//Ground: become kinematic so that it falls through the level
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag("Player"))
		{
			StartCoroutine(Fall());
		}

		if (col.collider.CompareTag("Ground"))
		{
			Debug.Log ("platform touching ground");
			GetComponent<Collider2D> ().isTrigger = true;
		}
	}
		
	//the code that makes the platform fall
	//applies velocity to the rigidbody2D
	IEnumerator Fall()
	{
		yield return new WaitForSeconds (fallDelay);
		rb2d.velocity = new Vector2 (0f, -5f);

		yield return 0;
	}
}
