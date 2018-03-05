/*
 *Following this tutorial:
 *https://www.youtube.com/watch?v=Y8nOgEpnnXo
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake> {

	private CameraShake(){}

	public Camera mainCam;

	public float shakeAmtX;
	public float shakeAmtY;

	float shakeAmt = 0;

	void Awake()
	{
		if (mainCam == null) {
			mainCam = Camera.main;
		}
	}
	/// <Michael>
    /// The actual camera shake function,
    /// Takes in a float for the x and y ammount to shake the camera
    /// </Michael>
	public void Shake(float amt, float length)
	{
		shakeAmt = amt;
		InvokeRepeating ("startShake", 0, 0.01f);
		Invoke ("stopShake", length);
	}
	///<Michael>
	///Test function to make sure shaking works,
	///Will shake the screen when the "t" key is pressed
	///</Michael>
	void Update(){
		if (Input.GetKeyDown (KeyCode.T)) {
			Shake (shakeAmtX, shakeAmtY);
		}
	}
	///<Michael>
	///Called when the Shake() function is called
	///moves the camera position based on the shake ammount
	///</Michael>
	void startShake()
	{
		if (shakeAmt > 0) {

			Vector3 camPos = mainCam.transform.position;

			float offsetX = Random.value * shakeAmt * 2 - shakeAmt;
			float offsetY = Random.value * shakeAmt * 2 - shakeAmt;
			camPos.x = offsetX;
			camPos.y = offsetY;
			camPos.z = 0f;

			mainCam.transform.localPosition = camPos;
		}
	}
	///<Michael>
	///Called to stop the shaking and reset the camera position
	///</Michael>
	void stopShake()
	{
		CancelInvoke ("startShake");
		mainCam.transform.localPosition = Vector3.zero;
	}
}

