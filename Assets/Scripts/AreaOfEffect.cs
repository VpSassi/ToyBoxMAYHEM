using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffect : MonoBehaviour
{
    public int aoeGunDamage = 2;
    PlayerMovement playerMov2;
	PewPew gunn;
    public float horizontalRange;
    public float verticalRange;
    public float cooldown;
    float timeSinceLastShot;
    Rigidbody2D rB;
    public LayerMask enemyMask;
    public bool penetrator;

    
    void Awake()
    {
        playerMov2 = GetComponent<PlayerMovement>();
		gunn = GetComponent<PewPew>();
        rB = GetComponent<Rigidbody2D>();
		
    }
    void Update()
    {

        bool facingRight = playerMov2.facingRight;
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.X) && playerMov2.onRope == false)
        {
            rB.freezeRotation = true;
            playerMov2.jumpingPermission = false;

            if (timeSinceLastShot > cooldown) // shoot!
            {

                timeSinceLastShot = 0;

                if (playerMov2.onRope == false)
                {
					penetrator = true;
                    var pos = transform.position;
                    if (facingRight == true)
                    {
                        Vector2 upperLeft = new Vector2(pos.x, pos.y + verticalRange / 2);
                        Vector2 lowerRight = new Vector2(pos.x + horizontalRange, pos.y - verticalRange / 2);

                        var Enemies = Physics2D.OverlapAreaAll(upperLeft, lowerRight, enemyMask);

                        //TODO vihollisille interface
                        //hit.collider.GetComponent<Zombear>().isHit = true;
                        //hit.collider.GetComponent<Zombear>().HP -= gunDamage;
                        foreach (Collider2D c in Enemies) {
							var z = c.GetComponent<Zombear>();
							z.isHit = true;
							c.GetComponent<Zombear>().HP -= aoeGunDamage;
							
                        }
                    } else if (facingRight == false)
                    {
                        Vector2 upperLeft = new Vector2(pos.x - horizontalRange, pos.y + verticalRange / 2);
                        Vector2 lowerRight = new Vector2(pos.x, pos.y - verticalRange / 2);

                        var Enemies = Physics2D.OverlapAreaAll(upperLeft, lowerRight, enemyMask);

                        //TODO vihollisille interface
                        //hit.collider.GetComponent<Zombear>().isHit = true;
                        //hit.collider.GetComponent<Zombear>().HP -= gunDamage;
                        foreach (Collider2D c in Enemies) {
							var z = c.GetComponent<Zombear>();
							z.isHit = true;
                            c.GetComponent<Zombear>().HP -= aoeGunDamage;
                        }
                    }
                }
                else {
                    rB.freezeRotation = true;
                    playerMov2.jumpingPermission = true;
					penetrator = false;
                }
            }
        }
        else {
            //  aOEL.enabled = false;
            //   aOER.enabled = false;
        }
    }
}