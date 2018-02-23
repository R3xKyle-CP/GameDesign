using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController> {

	public GameObject currentCheckpoint;

	public bool checkpointPassed;
	private PlayerController player;

	void Start()
	{
		player = FindObjectOfType<PlayerController> ();
		checkpointPassed = false;
	}

	public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        
    }

	public void RespawnPlayer()
	{
		Debug.Log ("Player Respawn here");

		if (!checkpointPassed) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		} 
		else
		{
			player.transform.position = currentCheckpoint.transform.position;
		}
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
