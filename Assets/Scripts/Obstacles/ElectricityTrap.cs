/*
 * Followed this tutorial
 *https://www.youtube.com/watch?v=3gXxRY3TGRg
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityTrap : MonoBehaviour {

    public GameObject electricity;
    public float onTime;
    public float offTime;

    Collider2D eCol;

	// Use this for initialization
	void Start () {
        eCol = GetComponent<Collider2D>();
		StartCoroutine(ElectricitySwitch());
	}
    ///<Michael>
    ///Turn the trap on and off after a certain amount of delay time
    ///</Michael>
    IEnumerator ElectricitySwitch(){
        while(true){
            yield return (new WaitForSeconds(offTime));
            eCol.enabled = true;
            electricity.SetActive(true);
            yield return (new WaitForSeconds(onTime));
            eCol.enabled = false;
            electricity.SetActive(false);
        }
    }

    ///<Michael>
    ///Damage the player if they collide with the trap while active
    ///</Michael>
    private void OnTriggerEnter2D()
    {
        PlayerController.Instance.PlayerHit(10);
    }
}
