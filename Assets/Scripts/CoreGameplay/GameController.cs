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
	public SimpleHealthBar healthBar;
	public SimpleHealthBar shieldBar;
	public Text memoryBarText;
    public GameObject cheatsOn;
    private GameController() { }
    public bool cheats = false;

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
        if(Input.GetKeyDown(KeyCode.C)){
            
            if(cheats==false){
                Debug.Log("cheats on");
                cheats = true;
                cheatsOn.SetActive(true);
            }else{
                Debug.Log("cheats off");
                cheats = false;
                cheatsOn.SetActive(false);
            }
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
				healthText.text = "Health: " + PlayerController.Instance.GetHealth ().ToString ();
				healthBar.UpdateBar (PlayerController.Instance.GetHealth (), 100.0f);
            	break;
            case BATTERY:
				batteryText.text = "Battery: " + PlayerController.Instance.GetBattery().ToString();
				shieldBar.UpdateBar (PlayerController.Instance.GetBattery (), 100.0f);
                break;
			case MEMORY:
				memoryText.text = "Memory: " + PlayerController.Instance.GetMemory().ToString();
				memoryBarText.text = "Memory: "+ PlayerController.Instance.GetMemory().ToString();
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
