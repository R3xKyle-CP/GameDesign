/*
 *Followed this tutorial:
 *https://www.youtube.com/watch?v=4R_AdDK25kQ
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedPlatform : MonoBehaviour
{

    private Vector3 posA;

    private Vector3 posB;

    private Vector3 nextPos;

    private bool activated = false;

    [SerializeField]
    public float movementSpeed;
    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    ///<Michael>
    ///Get the start and end positions that the platform will move to/from
    ///</Michael>
    void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated){
            Move(); 
        }
    }
    ///</Michael>
    ///Move the platform toward its next destination with the given movement speed
    ///</Michael>
    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDest();
        }
    }
    ///<Michael>
    ///Get the platforms next postion
    ///B if the current position is A, and A if the current position is B
    ///</Michael>
    private void ChangeDest()
    {
        nextPos = nextPos != posA ? posA : posB;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if(collision.gameObject.tag == "Player"){
            Debug.Log("activate");
            activated = true;
        }
	}
}
