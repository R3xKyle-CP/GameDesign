using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class imageController : MonoBehaviour {
	int current = 1;
	public Sprite s1;
	public Sprite s2;
	public Sprite s3;
	// Use this for initialization
	void Start () {
		this.GetComponent<Image>().sprite = s1;
	}
	
	// Update is called once per frame
	void Update () {
		if (current == 1) {
			this.GetComponent<Image>().sprite = s1;
		}
		if (Input.GetMouseButtonDown (0)) {
			current++;
			if (current >= 4) {
				PlayerController.Instance.fullBattery ();
				Destroy(gameObject);
			}
			if (current == 2) {
				this.GetComponent<Image>().sprite = s2;
			}
			if (current == 3) {
				this.GetComponent<Image>().sprite = s3;
			}
		}
	}
}
