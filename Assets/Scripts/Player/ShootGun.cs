using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : Singleton<ShootGun> {

	private ShootGun(){}

	public GameObject projectile;
	public Vector2 velocity;
	bool canShoot = true;
	public Vector2 offset = new Vector2(0.4f,0.1f);
	public float cooldown = 1f;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Y) && canShoot) {
			GameObject go = (GameObject)Instantiate (projectile,(Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

			go.GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocity.x * transform.localScale.x, velocity.y);

			StartCoroutine (CanShoot());

            if(!PlayerController.Instance.isGrounded){
                anim.Play("JumpShoot");
            }else{
                anim.Play("Shoot");
            }
		}

	}

	IEnumerator CanShoot(){
		canShoot = false;
		yield return new WaitForSeconds (cooldown);
		canShoot = true;
	}
}