using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TBound1 : MonoBehaviour {
    
    public GameObject Enabled;
    public GameObject Disabled;

    /// <Michael>
    /// Disables and Enables a new tutorial text
    /// </Michael>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            Enabled.SetActive(false);
            Disabled.SetActive(true);
        }
    }
}
