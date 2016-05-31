using UnityEngine;
using System.Collections;

public class SuppressiveFire : MonoBehaviour {


    public float distance = 100;
    public int sfGunDamage;
    PlayerMovement playerMov;
    public float SCDT;
    float timeSinceLastSCDT;
    Rigidbody2D rb;
    public LayerMask mask;
    public float pushForce;
    


    void Awake()
    {
        playerMov = GetComponent<PlayerMovement>();
        timeSinceLastSCDT = SCDT;
        rb = GetComponent<Rigidbody2D>();
    }

    void hitIn2Dir()
    {

        if (Input.GetKey(KeyCode.V))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
            playerMov.jumpingPermission = false;

            timeSinceLastSCDT += Time.deltaTime;
            if (timeSinceLastSCDT > SCDT)
            {
                timeSinceLastSCDT -= SCDT;

                if (playerMov.onRope == false)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "Enemy")
                        {
                            hit.collider.GetComponent<Zombear>().isHit = true;
                            hit.collider.GetComponent<Zombear>().HP -= sfGunDamage;
                            hit.collider.GetComponent<Rigidbody2D>().AddForce(Vector3.forward * pushForce);
                        }

                    }

                }
                else if (playerMov.onRope == false)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "Enemy")
                        {
                            hit.collider.GetComponent<Zombear>().isHit = true;
                            hit.collider.GetComponent<Zombear>().HP -= sfGunDamage;
                            hit.collider.GetComponent<Rigidbody2D>().AddForce(-Vector3.forward * pushForce);

                        }
                    }
                }
            }
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerMov.jumpingPermission = true;
        }
    }
}

        