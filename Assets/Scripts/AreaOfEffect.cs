using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffect : MonoBehaviour
{
    public List<GameObject> EnemiesInCollider;
    public float distance = 100;
    public int aoeGunDamage = 2;
    public PlayerMovement playerMov2;
    public float cooldown;
    float timeSinceLastShot;
    public Rigidbody2D rB;
    public LayerMask mask;
    public Animator penetrator;
    public BoxCollider2D aOER;
    public BoxCollider2D aOEL;


    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.gameObject.tag == "Enemy")
        {
            EnemiesInCollider.Add(c.gameObject);
        }
    }

    void Update()
    {

        bool facingRight = playerMov2.facingRight;
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.X))
        {
            rB.freezeRotation = true;
            playerMov2.jumpingPermission = false;

           
            if (timeSinceLastShot > cooldown) // shoot!
            {
                //penetrator.Play("PenetratorAnimator");
                timeSinceLastShot = 0;



                if ((facingRight == true && (transform.position.x >= playerMov2.transform.position.x))
                    && playerMov2.onRope == false)
                {
                    print("damage right");
                  //  aOER.enabled = true;
                    for (int i = 0; i < EnemiesInCollider.Count; i++)
                    {
                        print(i);
                        EnemiesInCollider[i].GetComponent<Zombear>().HP -= aoeGunDamage;

                    }

                    
                }

                if ((facingRight == false /*&& (transform.position.x <= playerMov2.transform.position.x)*/)
                 && playerMov2.onRope == false)
                {
                    print("damage left");
                  //  aOEL.enabled = true;
                    for (int i = 0; i < EnemiesInCollider.Count; i++)

                    {
                        print(i);
                        if (EnemiesInCollider[i] != null) 
                            EnemiesInCollider[i].GetComponent<Zombear>().HP -= aoeGunDamage;

                    }

                }
                else {
                    rB.freezeRotation = true;
                    playerMov2.jumpingPermission = true;

                }
            }
        } else {
          //  aOEL.enabled = false;
         //   aOER.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject.tag == "Enemy") 
      {
            EnemiesInCollider.Remove(c.gameObject);
       }
    } 
}