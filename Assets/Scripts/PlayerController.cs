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

    private float nextActionTime = 0.0f;

    // private components

    public bool isGrounded = false;
    public bool canMoveInAir = true;
    //private CharacterController2D controller;
    private Rigidbody2D myBody;
    private Transform myTrans,tagGround;
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
        nextActionTime = Time.time + (2 * batteryTimerInterval);
        GameController.Instance.PlayerAttributeUpdate(GameController.BATTERY);
        GameController.Instance.PlayerAttributeUpdate(GameController.HEALTH);
    }

    void Update()
    {
        if (health <= 0)
        {
            return;
        }
        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position,playerMask);
        float horizontalSpeed = Input.GetAxisRaw("Horizontal");

        Move(horizontalSpeed);
        Flip(horizontalSpeed);
        if (Input.GetButtonDown("Jump")){
            Jump();
            anim.Play("Jump");
        }

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

    public void Jump(){
        if(isGrounded){
            myBody.velocity += jumpHeight * Vector2.up;
        }

    }

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
		if(collision.gameObject.tag == "Cockroach" && collision.gameObject.GetComponent<Cockroach>().getLive()>0  && GameController.Instance.levelOver != true){
            PlayerHit(25);
        }
		if (collision.gameObject.tag == "Bat" && collision.gameObject.GetComponent<Bat>().getLive()>0 && GameController.Instance.levelOver != true)
        {
            PlayerHit(25);
        }
        if (collision.gameObject.tag == "Boundary")
        {
            anim.Play("Dead");
            GameController.Instance.levelOver = true;
            GameController.Instance.PlayerDied();
        }
        if(collision.gameObject.tag == "MovingPlatform"){
            myTrans.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            myTrans.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Battery" )
        {
            PickUpItemAttributeUpdate(ref battery, collision, GameController.BATTERY);
        }
        if(collision.gameObject.tag == "Spike"){
            Debug.Log("spike");
            myBody.velocity = new Vector2(0, 0);
            Vector2 pushBack = new Vector2(0, 7);
            myBody.AddForce(pushBack,ForceMode2D.Impulse);
            PlayerHit(10);
        }

    }

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

    void PickUpItemAttributeUpdate(ref int playerAttribute, Collider2D collider, int attribute)
    {
        Debug.Log("picking up the battery");
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