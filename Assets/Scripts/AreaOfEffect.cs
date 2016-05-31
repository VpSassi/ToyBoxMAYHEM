using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffect : MonoBehaviour
{
    public List<GameObject> EnemiesInCollider;
    public float distance = 100;
    public int aoeGunDamage = 2;
    bool facingRight;
    public PlayerMovement playerMov2;
    public float KCDT;
    float timeSinceLastKCDT;
    public Rigidbody2D rB;
    public LayerMask mask;
    public Animator penetrator;



    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.tag == "Enemy")
        {
            EnemiesInCollider.Add(c.gameObject);
        }
    }

    void Update() {

        facingRight = playerMov2.facingRight;

        if (Input.GetKey(KeyCode.X))
        {
            rB.constraints = RigidbodyConstraints2D.FreezePositionX;
            rB.freezeRotation = true;
            playerMov2.jumpingPermission = false;

            timeSinceLastKCDT += Time.deltaTime;
            if (timeSinceLastKCDT > KCDT)
            {
                penetrator.Play("PenetratorAnimator");
                timeSinceLastKCDT -= KCDT;

                if (facingRight == true && playerMov2.onRope == false)
                {
                    for (int i = 0; i < EnemiesInCollider.Count; i++)
                    {
                        EnemiesInCollider[i].GetComponent<Zombear>().HP -= aoeGunDamage;
                    }

                    print("Damage taken right");
                }   
                else if (facingRight == false && playerMov2.onRope == false)
                {
                    print("Damage taken left");
                }
            }
            else {
                rB.constraints = RigidbodyConstraints2D.FreezePositionX;
                rB.freezeRotation = true;
                playerMov2.jumpingPermission = true;
            }
        }
    }
}