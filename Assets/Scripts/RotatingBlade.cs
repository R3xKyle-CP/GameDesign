using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlade : MonoBehaviour {

    public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.Rotate(new Vector3(0,0,rotationSpeed));
	}

    private void OnTriggerEnter2D()
    {
        PlayerController.Instance.PlayerHit(25);
    }
}
