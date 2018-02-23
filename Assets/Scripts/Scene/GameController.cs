using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {

    public const int HEALTH = 1;
    public const int BATTERY = 2;

    public GameObject levelOverText;
    public Text healthText;
    public Text batteryText;
    public bool levelOver;

	//public variable for the SceneManager script, for respawning the player
	public SceneController sceneController;

    private GameController() { }

	void Start()
	{
		//finds object with scenemanager on it, and accesses it
		sceneController = FindObjectOfType<SceneController> ();
	}

    // Update is called once per frame
    void Update()
    {
        if (levelOver && Input.GetMouseButtonDown(0))
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			//sceneController.RespawnPlayer();
        }
    }	

    public void PlayerAttributeUpdate(int attribute)
    {
        switch (attribute)
        {
            case HEALTH:
                healthText.text = "Health: " + PlayerController.Instance.GetHealth().ToString();
                break;
            case BATTERY:
                batteryText.text = "Battery: " + PlayerController.Instance.GetBattery().ToString();
                break;
            default:
                break;

        }

    }


    public void PlayerDied()
    {
        levelOverText.SetActive(true);
        levelOver = true;

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");

        //Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			sceneController.RespawnPlayer ();
		}
	}
}
