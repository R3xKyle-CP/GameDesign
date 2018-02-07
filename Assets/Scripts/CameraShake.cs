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

	public void Shake(float amt, float length)
	{
		shakeAmt = amt;
		InvokeRepeating ("startShake", 0, 0.01f);
		Invoke ("stopShake", length);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.T)) {
			Shake (shakeAmtX, shakeAmtY);
		}
	}

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

	void stopShake()
	{
		CancelInvoke ("startShake");
		mainCam.transform.localPosition = Vector3.zero;
	}
}

