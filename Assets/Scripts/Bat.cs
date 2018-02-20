using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {
	private Animator anim;
	float horizontalSpeed;
	bool isDead = false;
	// float live = 100;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		//transform.LookAt(Player);
		/*if (Vector3.Distance (transform.position, Player.position) >= MinDist) {

			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
		}*/
		anim.Play ("idle");
		float distance = Vector3.Distance( Player.Instance.transform.position, transform.position);
		if (distance > 5) {
			Vector3 delta = Player.Instance.transform.position - this.transform.position;
			delta.Normalize ();
			float moveSpeed = 1 * Time.deltaTime;
			transform.position = transform.position + (delta * moveSpeed);
			horizontalSpeed = delta.x;

			//horizontalSpeed = (float)(Mathf.Sin (Time.realtimeSinceStartup) / 30.0);
			//transform.Translate (Vector3.left * horizontalSpeed);
			//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
			if (horizontalSpeed > 0f) {
				if (transform.localScale.x > 0f) {
					transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				}

			} else if (horizontalSpeed < 0f) {
				if (transform.localScale.x < 0f) {
					transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				}
			}
		}
		if (isDead) {
			anim.Play ("die");
			Destroy (this);
		}

	}
}
