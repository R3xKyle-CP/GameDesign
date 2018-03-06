using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
public class Cockroach : MonoBehaviour {
	    private Animator anim;
	    //private CharacterController2D controller;
	    float horizontalSpeed = 3f ;
	    public int moveright = 0;
	    bool isDead = false;
	    public Transform wallCheck;
	    public float wallCheckRadius;
	    private bool hittingWall;
	    public LayerMask whatIsWall;
	    private bool notAtEdge;
	    public Transform edgeCheck;
        private Collider2D col;
        private Rigidbody2D rb2d;
		float live = 100;
	    // Use this for initialization
	    void Start () {
		        anim = GetComponent<Animator>();
                col = GetComponent<Collider2D>();
                rb2d = GetComponent<Rigidbody2D>();
		        //controller = GetComponent<CharacterController2D>();

		}
	    
	    // Update is called once per frame
	    void Update () {
			if (live<=0) {
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
			            anim.Play ("Die");
                        col.enabled = false;
                        rb2d.gravityScale = 0;
			            //Destroy (this);
			        }
			else{
		        Vector3 delta = PlayerController.Instance.transform.position - this.transform.position;

				if (Mathf.Abs(delta.y)<10 &&  delta.x > 0.3 ) { //player is on the right, need to moveright
			            moveright = 1;


			            if (transform.localScale.x > 0f) {
				                transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				            }
			            hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
			            notAtEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);
			            if (hittingWall || !notAtEdge) {
				                GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
				                anim.Play ("Idle");
			            } else {
				                GetComponent<Rigidbody2D> ().velocity = new Vector2 (horizontalSpeed, GetComponent<Rigidbody2D> ().velocity.y);
				                anim.Play ("Walk");
				            }
		        } else if (delta.x < -0.3) { //player is on the left
			            moveright = -1;
			            if (transform.localScale.x < 0f ) {
				                transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				            }
			            hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
			            notAtEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);
			            if (hittingWall || !notAtEdge) {
				                GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
				                anim.Play ("Idle");
			            } else {
				                GetComponent<Rigidbody2D> ().velocity = new Vector2 (-horizontalSpeed, GetComponent<Rigidbody2D> ().velocity.y);
				                anim.Play ("Walk");        
				            }
			                
		        } else {
			            moveright = 0;
			            anim.Play ("Idle");
			    }
			}

		        /*
        if (hittingWall || !atEdge)
            moveright = !moveright;
        if (moveright) {
            //transform.Translate (Vector3.right);
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (transform.localScale.x > 0f) {
                transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            anim.Play ("Walk");
        } else {
            //transform.Translate (Vector3.left);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (transform.localScale.x < 0f ) {
                transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            anim.Play ("Walk");
        }
*/
	}
	public float getLive(){
		return this.live;
	}
	public void decreaseLive(){
		this.live -= 60;
		return;
	}
}

