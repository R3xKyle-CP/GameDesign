using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class Player : Singleton<Player> {

    // public variables affecting player movement
    public float jumpHeight = 3f;
    public float gravity = -2f;
    public float groundDamping = 20f; 
    public float airDamping = 5f;
    public float speed = 8f;
    public float maxSpeed = 8f;
    public float batteryTimerInterval = 100000000f;

	public GameObject projectile;
	public Vector2 bulletVelocity;
	bool canShoot = true;
	public Vector2 offset = new Vector2(0.4f,0.1f);
	public float cooldown = 1f;

    // private player attribute values
    private int battery;
    private int health;

    private float nextActionTime = 0.0f;

    // private components
    private CharacterController2D controller;
    private Animator anim;
    private Light light;
    private float lightIntesity;

    // private variables affecting player movement
    private Vector3 velocity;
    private float normalizedHorizontalSpeed = 0;

    private Player() { }

    void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        light = GetComponentInChildren<Light>();
        lightIntesity = light.intensity;

        controller.onControllerCollidedEvent += OnControllerColliderEvent;
        controller.onTriggerEnterEvent += OnTriggerEnter2DEvent;
        controller.onTriggerExitEvent += OnTriggerExit2DEvent;
    }

    void Start()
    {
        battery = 100;
        health = 100;
        nextActionTime = Time.time + (2 * batteryTimerInterval);
        GameController.Instance.PlayerAttributeUpdate(GameController.BATTERY);
        GameController.Instance.PlayerAttributeUpdate(GameController.HEALTH);
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            return;
        }
		if (Input.GetKeyDown(KeyCode.Y) && canShoot) {
			GameObject go = (GameObject)Instantiate (projectile,(Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

			go.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletVelocity.x * transform.localScale.x, bulletVelocity.y);

			StartCoroutine (CanShoot());

			anim.Play ("Shoot");
		}

        if (Time.time > nextActionTime)
        {
            battery = Mathf.Max(0, battery - 1);
            nextActionTime = Time.time + batteryTimerInterval;
            GameController.Instance.PlayerAttributeUpdate(GameController.BATTERY);
			if (battery <= 0) {
                light.intensity = 0f;
				GameController.Instance.PlayerDied();
			}
        }

        if (battery >= 75)
        {
            light.intensity = lightIntesity;
        }
        else if (battery >= 50 && battery < 75)
        {
            light.intensity = lightIntesity * .8f;
        }
        else if (battery >= 25 && battery < 50)
        {
            light.intensity = lightIntesity * .6f;
        }
        else if (battery <= 25 && battery > 0)
        {
            light.intensity = lightIntesity * .4f;
        }


        float horizontalSpeed = Input.GetAxis("Horizontal");


        if (controller.isGrounded)
        {
            velocity.y = 0;
        }

        if (horizontalSpeed < 0f)
        {
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
			if (controller.isGrounded) {
				anim.Play("Run");
			}
            
        }
        else if (horizontalSpeed > 0f)
        {
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
			if (controller.isGrounded) {
				anim.Play("Run");
			}
        }
        else
        {
            normalizedHorizontalSpeed = 0;
			if (controller.isGrounded) {
				anim.Play("Idle");
			}
        }

        if (Input.GetKeyDown("space") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            anim.Play("Jump");
        }

        /*
        if (controller.isGrounded && Input.GetAxis("Vertical") < 0f)
        {
            // do stuff here
            anim.Play("Slide");
        }
        */

        float smoothMovementFactor;

        if (controller.isGrounded)
        {
            smoothMovementFactor = groundDamping;
        }
        else
        {
            smoothMovementFactor = airDamping;
        }

		//velocity.x = Mathf.SmoothDamp(velocity.x, normalizedHorizontalSpeed * speed, ref velocity.x, Time.deltaTime * smoothMovementFactor, maxSpeed);
		velocity.x = Mathf.Lerp(velocity.x, normalizedHorizontalSpeed * speed, Time.deltaTime * smoothMovementFactor);
        // apply gravity
        velocity.y += gravity * Time.deltaTime;

        // can implement one way platforms if we would like by doing this
        /*
         * if (controller.isGrounded && (Input.GetAxis("vertical") < 0f))
         * {
         * velocity.y *= 3f;
         * controller.ignoreOneWayPlatformsThisFrame = true;
         * }
        */ 

        controller.move(velocity * Time.deltaTime);

        velocity = controller.velocity;

    }

    public int GetHealth()
    {
        return health;
    }

    public int GetBattery()
    {
        return battery;
    }

    void PickUpItemAttributeUpdate(ref int playerAttribute, Collider2D collider, int attribute)
    {
        if (playerAttribute >= 100)
        {
            return;
        }

        PickUp pickUp = collider.GetComponent<PickUp>();
        playerAttribute = Mathf.Min(100, playerAttribute + pickUp.GetValue());
        GameController.Instance.PlayerAttributeUpdate(attribute);
        Destroy(collider.gameObject);
    }

    void PlayerHit(int damageValue)
    {
        health = Mathf.Max(0, health - damageValue);
        GameController.Instance.PlayerAttributeUpdate(GameController.HEALTH);
        
        if (health <= 0)
        {
            GameController.Instance.levelOver = true;
            anim.Play("Dead");
        }
    }

    void OnControllerColliderEvent(RaycastHit2D raycastHit)
    {

        if(raycastHit.collider.gameObject.tag == "Enemy"){
            PlayerHit(5);
        }
    }

    void OnTriggerEnter2DEvent(Collider2D collider)
    {
		if (collider.gameObject.tag == "Battery") {
			PickUpItemAttributeUpdate (ref battery, collider, GameController.BATTERY);
            Debug.Log("Got a battery");
		} else if (collider.gameObject.tag == "Health") {
			PickUpItemAttributeUpdate (ref health, collider, GameController.HEALTH);
		} 
		else if (collider.gameObject.tag == "Enemy") {
			Debug.Log("HIT AN ENEMY");
		}
    }
	/*void enemyCollisionAttributeUpdate(ref int playerAttribute, Collider2D collider, int attribute){
		if (playerAttribute <= 0)
		{
			return;
		}
		PickUp pickUp = collider.GetComponent<PickUp>();
		playerAttribute = Mathf.Max(0, playerAttribute - pickUp.GetValue());
		GameController.Instance.PlayerAttributeUpdate(attribute);
	}*/
    void OnTriggerExit2DEvent(Collider2D collider)
    {

    }

	IEnumerator CanShoot(){
		canShoot = false;
		yield return new WaitForSeconds (cooldown);
		canShoot = true;
	}
}
