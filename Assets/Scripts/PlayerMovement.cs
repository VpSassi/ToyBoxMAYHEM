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

    public float dashCDT;

    void Start() {
        rb = GameObject.Find("Bunny").GetComponent<Rigidbody2D>();
        jumpingPermission = true;
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

        if (Input.GetKeyDown(KeyCode.C)) {

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

