using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {

    public const int HEALTH = 1;
    public const int BATTERY = 2;
	public const int MEMORY = 3;
    public GameObject levelOverText;
    public Text healthText;
    public Text batteryText;
	public Text memoryText;
    public bool levelOver = false;

    private GameController() { }

    ///</Michael>
    ///If the player is dead, the can press the mouse button to restart the level
    ///Resets the physics layers so that the player takes damage from the obstacles again
    ///</Michael>
    void Update()
    {
        if (levelOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            levelOver = false;
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer,false);
        }
    }	
    ///<Kyle>
    ///Updates the players battery and health value
    ///</Kyle>
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
			case MEMORY:
				memoryText.text = "Memory: " + PlayerController.Instance.GetMemory().ToString();
				break;
            default:
                break;

        }

    }

    ///<Michael>
    ///Display the level over text when the player dies
    ///Disable the collisions between players and enemies 
    ///</Michael>
    public void PlayerDied()
    {
        levelOverText.SetActive(true);
        levelOver = true;

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");

        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
    }
}
