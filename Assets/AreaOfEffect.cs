using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffect : MonoBehaviour
{
    public List<GameObject> EnemiesInCollider;
    public float distance = 100;
    public int aoeGunDamage = 2;
    bool facingRight;
    public PlayerMovement playerMov;
    public float KCDT;
    float timeSinceLastKCDT;
    //CDT = CoolDownTime
    public Rigidbody2D rb;
    public LayerMask mask;

    // public float blastRadius = 50;


    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.tag == "Enemy")
        {
            EnemiesInCollider.Add(c.gameObject);
        }
    }

    void Update() {

        facingRight = playerMov.facingRight;

        if (Input.GetKey(KeyCode.X))
        {

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
            playerMov.jumpingPermission = false;

            timeSinceLastKCDT += Time.deltaTime;
            if (timeSinceLastKCDT > KCDT)
            {
                timeSinceLastKCDT -= KCDT;

                if (facingRight == true && playerMov.onRope == false)
                {
                    for (int i = 0; i < EnemiesInCollider.Count; i++)
                    {
                        EnemiesInCollider[i].GetComponent<Zombear>().HP -= aoeGunDamage;
                    }

                    print("Damage taken right");
                }
                else if (facingRight == false && playerMov.onRope == false)
                {
                    print("Damage taken left");
                }
            }
            else {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                playerMov.jumpingPermission = true;
            }
        }
    }
}