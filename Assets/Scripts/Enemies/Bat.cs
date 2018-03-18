//Jiaqing
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {
	private Animator anim;
    private Collider2D col;
	float horizontalSpeed;
	float live = 100;
	bool following = false;
    public float speed;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update() {
		//transform.LookAt(Player);
		/*if (Vector3.Distance (transform.position, Player.position) >= MinDist) {

			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
		}*/
		if (live <= 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
			anim.Play ("die");
            col.enabled = false;
			Destroy (this.gameObject,2);
		} else {
			anim.Play ("idle");
			float distance = Vector3.Distance (PlayerController.Instance.transform.position, transform.position);
			if ((distance != 0 && distance<15) || following) {
				following = true;
				Vector3 delta = PlayerController.Instance.transform.position - this.transform.position;
				delta.Normalize ();
				float moveSpeed = speed * Time.deltaTime;

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
		}


	}
	public float getLive(){
		return this.live;
	}
	public void decreaseLive(){
		this.live -= 60;
		return;
	}
}
