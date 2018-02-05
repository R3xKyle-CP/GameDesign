using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		transform.Translate(Vector3.left * Mathf.Sin(Time.realtimeSinceStartup)/100);
			//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
	}

}
