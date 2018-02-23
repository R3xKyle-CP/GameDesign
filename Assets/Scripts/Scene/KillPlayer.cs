using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

	//public variable for the SceneManager script, for respawning the player
	public SceneController sceneController;

	// Use this for initialization
	void Start () {
		//finds object with scenemanager on it, and accesses it
		sceneController = FindObjectOfType<SceneController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			sceneController.RespawnPlayer ();
		}
	}
}
