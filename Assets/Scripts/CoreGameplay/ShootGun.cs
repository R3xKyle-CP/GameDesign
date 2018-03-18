using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : Singleton<ShootGun> {

	private ShootGun(){}

	public GameObject projectile;
	//public Vector2 velocity;
	public float velocity = 5f;
	bool canShoot = true;
	public Vector2 offset = new Vector2(0.4f,0.1f);
	public float cooldown = 1f;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	///<MichaelJiaqing>
	///Shoot a bullet when the right mouse button is clicked
	///Bullet is shot towards the direction of the mouse cursor
	///Michael did shooting logic, instantiating the bullet
	///Jiaqing did the shoot on right clicj and shoot towards cursor
	///</MichaelJiaqing>
	void Update () { 
		/* shoot the bullet in the direction of the right mouse click*/
		if (Input.GetMouseButtonDown(1) && canShoot ){
			Vector3 shootDirection;
			shootDirection = Input.mousePosition;
			shootDirection.z = 0.0f;
			shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
			shootDirection = shootDirection-transform.position;

			//...instantiating the bullet
			GameObject bulletInstance = (GameObject)Instantiate(projectile, transform.position + (Vector3)(shootDirection * 0.5f), Quaternion.Euler(new Vector3(0,0,0))) ;


			bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * velocity, shootDirection.y * velocity);
			StartCoroutine (CanShoot());

            if(!PlayerController.Instance.isGrounded){
                anim.Play("JumpShoot");
            }else{
                anim.Play("Shoot");
            }
		}

	}

	///<Michael>
	///Cooldown for shooting
	///</Michael>
	IEnumerator CanShoot(){
		canShoot = false;
		yield return new WaitForSeconds (cooldown);
		canShoot = true;
	}
}