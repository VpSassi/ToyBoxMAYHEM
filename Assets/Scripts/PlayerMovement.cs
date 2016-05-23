using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jump;
    float movementVelocity;
    public bool grounded = false;
    public bool facingRight = true;
    Rigidbody2D  rb;
    public bool onRope = false;
    public bool jumpingPermission;

    public float dashForce;
    public float dashTimer;
    bool isDashing;
    public float dashDuration;
    bool dashDirection;

    Zombear zb;
    public bool playerIsHit;
    public int playerHP;



    void Start() {
        rb = GameObject.Find("Bunny").GetComponent<Rigidbody2D>();
        zb = GameObject.Find("Zombear").GetComponent<Zombear>();
        jumpingPermission = true;
    }


    void Update () {

        if (isDashing == true) {
        dashTimer += Time.deltaTime;

            if (dashTimer < dashDuration && dashDirection == true) {
                rb.MovePosition(transform.position + Vector3.right * dashForce * Time.deltaTime);
            }
            else if(dashTimer < dashDuration && dashDirection == false) {
                rb.MovePosition(transform.position + Vector3.left * dashForce * Time.deltaTime);
            }
            else {
                isDashing = false;
                dashTimer = 0;
            }
        }

        //dash += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C)) {

            dashDirection = facingRight;
            if (onRope == false)
            {
               isDashing = true;
            }
        }
        if (playerIsHit == true) {
            playerHP -= zb.enemyDMG;
        }
        if (playerHP == 0) {
            print("Player dead");
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && jumpingPermission == true)
        {
            onRope = false;
            rb.isKinematic = false;
            if (grounded)
            
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                GetComponent<Rigidbody2D>().velocity.x, jump);
            }
        }
        movementVelocity = 0;
        if (onRope == false)
        {

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movementVelocity = -speed;
            facingRight = false;
            
            //transform.rotation =
            //Quaternion.LookRotation(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movementVelocity = speed;
            facingRight = true;
            //transform.rotation =
            //Quaternion.LookRotation(new Vector3(1, 0, 0));
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(movementVelocity,
                                               GetComponent<Rigidbody2D>().velocity.y);
        grounded = false;
    }
        if (onRope == true)
        {
            //rb.isKinematic = true;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movementVelocity = speed;
                
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movementVelocity = -speed;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, movementVelocity);
        }


        
    }
    //void OnTriggerEnter2D()
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == ("Rope") && Input.GetKey(KeyCode.UpArrow))
        {
            rb.isKinematic = true;
            onRope = true;
        }

        grounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Rope"))
        {
            rb.isKinematic = false;
            onRope = false;
        }
      
    //    grounded = false;
    }
    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == ("Rope") && Input.GetKey(KeyCode.UpArrow))
        {
            rb.isKinematic = true;
            onRope = true;
        }
    }


    //Physics2D.OverlapCircle -- Katso unity scripting referencestä
    

}

