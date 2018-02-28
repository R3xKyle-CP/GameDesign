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

    private GameController() { }

    // Update is called once per frame
    void Update()
    {
        if (levelOver && Input.GetMouseButtonDown(0))
        {
            levelOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
    }
}
