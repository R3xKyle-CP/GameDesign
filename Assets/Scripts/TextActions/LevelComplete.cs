using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public GameObject levelOverText;

    /// <Michael>
    /// Disables and Enables a new tutorial text
    /// </Michael>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelOverText.SetActive(true);
        }
    }
}