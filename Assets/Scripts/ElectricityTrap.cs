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

    private void OnTriggerEnter2D()
    {
        PlayerController.Instance.PlayerHit(25);
    }
}
