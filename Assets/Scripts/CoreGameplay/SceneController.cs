//Kyle wrote this script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController> {

	public GameObject player;
	public GameObject doorEntrance;
	public float currentSpawnX;
	public float currentSpawnY;

	void Start()
	{
		Instantiate (player, new Vector3(doorEntrance.transform.position.x, doorEntrance.transform.position.y, 0f), Quaternion.identity);
		currentSpawnX = doorEntrance.transform.position.x;
		currentSpawnY = doorEntrance.transform.position.y;
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void SpawnPlayer(float x, float y)
	{
		Instantiate (player, new Vector3(x, y, 0f), Quaternion.identity);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
