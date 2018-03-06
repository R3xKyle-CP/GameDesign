using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    // public variables affecting player movement
    public float jumpHeight = 10f;
    public float gravity = -2f;
    public float speed = 8f;
    public float maxSpeed = 8f;
    public float batteryTimerInterval = 0.5f;

    public LayerMask playerMask;

    public float hurtTime = 3f;

    // private player attribute values
    private int battery;
    private int health;
	private int memory;
    private float nextActionTime = 0.0f;

    // private components

    public bool isGrounded = false;
    private bool canMoveInAir = true;
    //private CharacterController2D controller;
    private Rigidbody2D myBody;
    public Transform myTrans,tagGround;
    //private Collider2D playerCollider;
    private Animator anim;
    private Light light;
    private float lightIntesity;
    private bool facingRight;

    // private variables affecting player movement
    //private Vector3 velocity;

    //private float normalizedHorizontalSpeed = 0;

    private PlayerController() { }

    public Collider2D[] myColls;

    ///Get the components
    void Awake()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.GetComponent<Transform>();
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        myColls = this.GetComponents<Collider2D>();
        anim = GetComponent<Animator>();
        light = GetComponentInChildren<Light>();
        lightIntesity = light.intensity;

    }

    void Start()
    {
        facingRight = true;
        battery = 100;
        health = 100;
		memory = 0;
        nextActionTime = Time.time + (2 * batteryTimerInterval);
        GameController.Instance.PlayerAttributeUpdate(GameController.BATTERY);
        GameController.Instance.PlayerAttributeUpdate(GameController.HEALTH);
		GameController.Instance.PlayerAttributeUpdate(GameController.MEMORY);
    }

    void Update()
    {
        if (health <= 0)
        {
            return;
        }

        ///<Michael>
        ///Use a linecast to determine if the player is grounded or not
        ///Get "horizontal" input from player and call the Move() function to move player
        ///Flip the player when changing directions
        ///</Michael>
        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position,playerMask);
        float horizontalSpeed = Input.GetAxisRaw("Horizontal");
        Move(horizontalSpeed);
        Flip(horizontalSpeed);

        ///Call jump function when "Jump" input is pressed
        if (Input.GetButtonDown("Jump")){
            Jump();
            anim.Play("Jump");
        }

        ///<Kyle>
        ///Decrement the battery as time goes one, and update the value
        ///Decrease the light as the battery dies
        ///If the battery runs out the player is dead
        ///</Kyle>
        if (Time.time > nextActionTime) 
        {
            battery = Mathf.Max(0, battery - 1);
            nextActionTime = Time.time + batteryTimerInterval;
            GameController.Instance.PlayerAttributeUpdate(GameController.BATTERY);
            if (battery <= 0)
            {
                light.intensity = 0f;
                anim.Play("Dead");
                GameController.Instance.levelOver = true;
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
    }
    ///<Michael>
    ///Move the player and call the animations corresponding to the movement
    ///</Michael>
    public void Move(float horizontalInput)
    {
        if(!canMoveInAir && !isGrounded)
        {
            return;
        }
        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalInput * speed;
        myBody.velocity = moveVel;

        if (horizontalInput < 0f)
        {
            //normalizedHorizontalSpeed = -1;
            if (isGrounded)
            {
                anim.Play("Run");
            }

        }
        else if (horizontalInput > 0f)
        {
            //normalizedHorizontalSpeed = 1;
            if (isGrounded)
            {
                anim.Play("Run");
            }

        }
        else
        {
            //normalizedHorizontalSpeed = 0;
            if (isGrounded)
            {
                anim.Play("Idle");
            }
        }


    }

    ///<Michael>
    ///Jumo function, add a verticle velocity to the player
    ///</Michael>
    public void Jump(){
        if(isGrounded){
            myBody.velocity += jumpHeight * Vector2.up;
        }

    }
    ///<Michael>
    ///Flip the player sprite when changing directions
    ///</Michael>
    private void Flip(float horizontal){
        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)){
            facingRight = !facingRight;
            Vector3 theScale = myTrans.localScale;

            theScale.x *= -1;

            myTrans.localScale = theScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ///<Annie>
        ///
        ///</Annie>
		if(collision.gameObject.tag == "Cockroach" && collision.gameObject.GetComponent<Cockroach>().getLive()>0  && GameController.Instance.levelOver != true){
            PlayerHit(25);
        }
		if (collision.gameObject.tag == "Bat" && collision.gameObject.GetComponent<Bat>().getLive()>0 && GameController.Instance.levelOver != true)
        {
            PlayerHit(25);
        }
        ///<Michael>
        ///If the player goes outside the play zone the die
        ///</Michael>
        if (collision.gameObject.tag == "Boundary")
        {
            anim.Play("Dead");
            GameController.Instance.levelOver = true;
            GameController.Instance.PlayerDied();
        }
        ///</Michael>
        ///If the player touches a moving platform, they
        ///become a child of the platform, moving along with it
        ///</Michael>
        if(collision.gameObject.tag == "MovingPlatform"){
            Debug.Log("on a moving platform");
            myTrans.parent = collision.gameObject.transform;
        }
    }
    ///</Michael>
    ///When the player leaves moving platform they are not
    /// longer a child
    ///</Michael>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            myTrans.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ///<Kyle>
        ///update the player battery level when a battery is picked up
        ///</Kyle>
        if (collision.gameObject.tag == "Battery" )
        {
            PickUpItemAttributeUpdate(ref battery, collision, GameController.BATTERY);
        }
		if (collision.gameObject.tag == "Disk" )
		{
			PickUpItemAttributeUpdate(ref memory, collision, GameController.MEMORY);
		}
        ///<Michael>
        ///if the player collides with a spike of blade, the take damage and bounce up
        ///</Michael>
        if(collision.gameObject.tag == "Spike"){
            Debug.Log("spike");
            myBody.velocity = new Vector2(0, 0);
            Vector2 pushBack = new Vector2(0, 7);
            myBody.AddForce(pushBack,ForceMode2D.Impulse);
            PlayerHit(10);
        }

        if (collision.gameObject.tag == "Blade")
        {
            myBody.velocity = new Vector2(0, 0);
            Vector2 pushBack = new Vector2(0, 7);
            myBody.AddForce(pushBack, ForceMode2D.Impulse);
            PlayerHit(25);
        }

    }

    ///<Michael>
    ///When the player takes damage, call hurtblinker,
    ///play the blink animation
    ///update the player health
    ///check if the player is dead
    ///</Michael>
    public void PlayerHit(int damageValue)
    {
        StartCoroutine(HurtBlinker());
        health = Mathf.Max(0, health - damageValue);
        CameraShake.Instance.Shake(0.2f,0.3f);
        anim.Play("alphaBlink");
        GameController.Instance.PlayerAttributeUpdate(GameController.HEALTH);
       
        if (health <= 0)
        {
            GameController.Instance.levelOver = true;
            GameController.Instance.PlayerDied();
            anim.Play("Dead");
        }

    }

    ///<Michael>
    ///give the player a couple of seconds when they are damaged 
    ///where they are not able to be damaged by anything,
    ///invincibility frames in essence
    ///</Michael>
    IEnumerator HurtBlinker()
    {
        //Ignore collisions between enemy and players

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");

        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);


        //Restart colliders so player can go through enemy after getting damaged
        //myColls is an array of all of the player's colliders
        foreach (Collider2D col in myColls)
        {
            col.enabled = false;
            col.enabled = true;
        }

        //blink animations


        //Re-Enable collisions after wait time
        yield return new WaitForSeconds(hurtTime);
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer,false);


    }

    public int GetHealth()
    {
        return health;
    }

    public int GetBattery()
    {
        return battery;
    }
	public int GetMemory()
	{
		return memory;
	}
    ///<Kyle>
    ///update the player values when something is picked up
    ///</Kyle>
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


}