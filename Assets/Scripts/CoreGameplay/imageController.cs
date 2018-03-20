/// <Jiaqing >
/// control the stort pictures
/// </Jiaqing>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class imageController : Singleton<imageController>{
	public Image s1;
	bool displaying = false;

	void Start () {
		s1.enabled=false;
	}

	void Update(){
		if (displaying && Input.GetMouseButtonDown(0))
			Destroy (gameObject);
	}
	public void showPicture(){
		s1.enabled=true;
		displaying = true;
	}

}

