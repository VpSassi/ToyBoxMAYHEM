using UnityEngine;
using System.Collections;

public class PewPew : MonoBehaviour {

    public float distance = 100;
    public int gunDamage = 1;
    bool facingRight;
    PlayerMovement playerMov;
    public float CDT;
    float timeSinceLastCDT;
    //CDT = CoolDownTime
    Rigidbody2D rb;
    public LayerMask mask;

    void Awake() {
        playerMov = GetComponent<PlayerMovement>();
        timeSinceLastCDT = CDT;
        rb = GetComponent<Rigidbody2D>();
    }

	void Update() {
        facingRight = playerMov.facingRight;

        if (Input.GetKey(KeyCode.Z)) {

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
            playerMov.jumpingPermission = false;

        timeSinceLastCDT += Time.deltaTime;
        if (timeSinceLastCDT > CDT) {
            timeSinceLastCDT -= CDT;

            if (facingRight == true && playerMov.onRope == false) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 14, mask.value);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "Enemy") {
                            hit.collider.GetComponent<Zombear>().HP -= gunDamage;
                            print("enemy hit");
                        }
                        print("HitRight");
                    }
                    

                }
            else if (facingRight == false && playerMov.onRope == false) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 14, mask.value);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "Enemy")
                        {
                            hit.collider.GetComponent<Zombear>().HP -= gunDamage;
                            print("enemy hit");
                        }
                        print("HitLeft");
                    }
                        //Debug.DrawRay(transform.position, backwards, Color.white);
                }
          }
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerMov.jumpingPermission = true;

        }



    }
}
