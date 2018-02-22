using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private Vector3 posA;

    private Vector3 posB;

    private Vector3 nextPos;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

	// Use this for initialization
	void Start () {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

    private void Move(){
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, movementSpeed * Time.deltaTime);
        if(Vector3.Distance(childTransform.localPosition,nextPos)<=0.1){
            ChangeDest();
        }
    }

    private void ChangeDest(){
        nextPos = nextPos != posA ? posA : posB;
    }
}
