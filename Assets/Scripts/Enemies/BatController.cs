//Jiaqing
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour {
	private Animator anim;
	float horizontalSpeed;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update() {
		
		transform.Translate(Vector3.left * Mathf.Sin(Time.realtimeSinceStartup)/100);
			//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
		if (horizontalSpeed < 0f) {
			if (transform.localScale.x > 0f) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Walk");
		} else if (horizontalSpeed > 0f) {
			if (transform.localScale.x < 0f) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
		}
	}

}
