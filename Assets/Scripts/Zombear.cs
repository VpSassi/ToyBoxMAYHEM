using UnityEngine;
using System.Collections;

public class Zombear : MonoBehaviour
{

    public int HP = 3;
    PewPew pewPew;
    PlayerMovement pm;
    AreaOfEffect areaOfEffect;

    public bool isHit;
    public bool getsHit;
    public float recovery;
    public float recoveryCDT;
    public float hitForce;

    public int enemyDMG;
    public float attack;
    public float attackCDT;
    public bool attacking;
    public float pPushForce;
	public float deathTime;

    SpriteRenderer sr;
    AreaOfEffect aOER;
    AreaOfEffect aOEL;
    BoxCollider2D bcR;
    BoxCollider2D bcL;

    Rigidbody2D rbEnemy;

    public float enemySpeed;
    public Transform target;
    public bool isChasing;
    Vector3 facingDirection;

    public Animator zombear;

    void Awake()
    {
        pewPew = GetComponent<PewPew>();
        sr = GetComponent<SpriteRenderer>();
        pm = GameObject.Find("Bunny").GetComponent<PlayerMovement>();
        isChasing = true;
        target = pm.transform;


    }

    void Update()
    {

        if (isChasing == true && pm.playerHP > 0)
        {
            float step = enemySpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target.position, step);


        }

        facingDirection = (target.position - transform.position).normalized;
        if ( facingDirection.x > 0) {
            sr.flipX = false;
        }
        else {
            sr.flipX = true;
        }




        if (isHit == true)
        {
            recovery += Time.deltaTime;
            if (recovery > recoveryCDT)
            {
                recovery -= recoveryCDT;
                isHit = false;
				GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, hitForce);
            }

        }

        if (HP <= 0)
        {
			isChasing = false;
			attacking = false;
			deathTime += Time.deltaTime;

			if (deathTime > 1.5) {
            Destroy(gameObject);
			}

        }

        if (attacking == true && pm.playerHP > 0)
        {

            attack += Time.deltaTime;
            if (attack > attackCDT)
            {
                pm.playerIsHit = true;
                attack -= attackCDT;
            }
            else pm.playerIsHit = false;
        }



    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            attacking = true;
        }

        if (c.tag == "AreaOfEffectRight")

        {
            //rbEnemy.MovePosition(transform.position + Vector3.up * hitForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, hitForce);


        }

        if (c.tag == "AreaOfEffectLeft")

        {
            //rbEnemy.MovePosition(transform.position + Vector3.up * hitForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, hitForce);

        }

    }
    void OnTriggerExit2D(Collider2D c)
    {

        if (c.tag == "Player")
        {
            attacking = false;
        }

        if (c.tag == "AreaOfEffectRight")
        {
            recovery += Time.deltaTime;
            if (recovery > recoveryCDT)
            {
                recovery -= recoveryCDT;
                isHit = false;
            }

            if (c.tag == "AreaOfEffectLeft")
            {
                recovery += Time.deltaTime;
                if (recovery > recoveryCDT)
                {
                    recovery -= recoveryCDT;
                    isHit = false;
                }
            }
        }
    }
}