using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlade : MonoBehaviour {

    public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	///<Michael>
	///Rotate the blade
	///</Michael>
	void FixedUpdate () {
		this.transform.Rotate(new Vector3(0,0,rotationSpeed));
	}

    /*private void OnTriggerEnter2D()
    {
        Debug.Log("hit!");
        PlayerController.Instance.PlayerHit(25);
    }*/
}
